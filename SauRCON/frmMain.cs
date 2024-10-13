//#define USE_TEST_DATA

using CoreRCON;
using CoreRCON.Parsers.OHD;
using SauRCON.SaveGame;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SauRCON
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();

            // create an instance of a ListView column sorter and assign it to the ListView control
            lvwColumnSorter = new ListViewColumnSorter() { Order = SortOrder.Ascending, SortColumn = 0 };
            this.lvPlayers.ListViewItemSorter = lvwColumnSorter;

            // initialise command auto-complete
            tbCommand.AutoCompleteCustomSource = ReadAutoCompleteFile();

            // hook link status events
            SauronLink.OnInLinkStatus += (status) =>
            {
                // invoke on the main thread because these are UI changes
                Invoke((MethodInvoker)(() =>
                {
                    SetLinkStatus(lbLinkIn, "In", status);

                    // refresh as we might have new information
                    Update(mCurrentStatus);
                }));
            };
            SauronLink.OnOutLinkStatus += (status) =>
            {
                // invoke on the main thread because these are UI changes
                Invoke((MethodInvoker)(() =>
                {
                    SetLinkStatus(lbLinkOut, "Out", status);
                }));
            };

            // hook link input object validation
            SauronLink.OnValidateInputObject = (obj) =>
            {
                if (mCurrentStatus != null)
                {
                    if (!string.IsNullOrEmpty(obj.ServerName))
                    {
                        // XXX: TODO ... put this back in when we fix the server output in r14!

                        // check that server name matches
                        return string.Compare(obj.ServerName, mCurrentStatus.ServerName, true) == 0;
                    }
                    else
                    {
                        // we don't have server name in the object
                        return true;
                    }
                }

                // validation failure
                return false;
            };

            // hook player mapping event
            SauronLink.OnPlayerMapping += (playerMapping) =>
            {
                lock (sNameLookUpMutex)
                {
                    sNameLookUp = playerMapping;
                }
            };
            
            // hook player selection event
            SauronLink.OnSelectedPlayer += (id, at) =>
            {
                // invoke on the main thread because these are UI changes
                Invoke((MethodInvoker)(() =>
                {
                    // check id is valid and event is chromologically after our last selection data
                    if (id > 0 && at > mSelectedPlayerAt)
                    {
                        var selectedItem = SelectedPlayerItem;

                        // check we have items and the UI needs to change
                        if (lvPlayers.Items.Count > 0 && (selectedItem == null || id != selectedItem.Id))
                        {
                            // find item
                            var item = lvPlayers.Items[id.ToString()];
                            if (item != null)
                            {
                                // select item
                                item.Selected = true;

                                // focus item and ensure it's scrolled into view
                                lvPlayers.FocusedItem = item;
                                lvPlayers.EnsureVisible(item.Index);
                                lvPlayers.Focus();
                            }
                        }

                        // record latest selection data
                        mSelectedPlayerId = id;
                        mSelectedPlayerAt = at;
                    }
                }));
            };
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
#if !USE_TEST_DATA
            _ = Connect();
#else
            /* TESTING USING FILE DATA TO SIMULATE RCON */
            SauronLink.UsingTestData = true;
            SauronLink.SauronImport();
            var parser = new StatusParser();
            var status = parser.Parse(Properties.Resources.status);
            Update(status);
            //SauronLink.SauronImport();
#endif
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            Disconnect();
        }

        private void lvPlayers_DoubleClick(object sender, EventArgs e)
        {
            var selectedItem = SelectedPlayerItem;
            if (selectedItem != null && selectedItem.Type == PlayerType.Human)
            {
                Clipboard.SetText(selectedItem.NetworkIdString);

                var url = $"https://www.steamidfinder.com/lookup/{selectedItem.NetworkId}/";
                Process.Start(url);
            }
        }

        private void tbCommand_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (mConnection != null && mConnection.Connected)
                {
                    var command = tbCommand.Text.Trim();
                    tbCommand.Text = string.Empty;
                    SendCommand(command);
                }
            }
        }

        private async Task Connect()
        {
            Cursor = Cursors.WaitCursor;

            try
            {
                var host = tbHost.Text.Trim();
                var port = int.Parse(tbPort.Text.Trim());
                var password = tbPassword.Text.Trim();

                // lock UI
                btnConnect.Enabled = false;

                // connect to the server
                var addresses = Dns.GetHostAddresses(host);
                var ip = addresses.First(address => address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
                var rcon = new CoreRCON.RCON(new IPEndPoint(ip, port), password, autoConnect: false, dynamicMultiPacketSupport: true);
                rcon.OnDisconnected += () =>
                {
                    Invoke((MethodInvoker)(() =>
                    {
                        Disconnect();
                    }));
                };
                await rcon.ConnectAsync();

                if (!rcon.Connected)
                {
                    throw new Exception($"Connect to {ip}:{port} completed but connection is not marked as 'Connected'?");
                }

                if (!rcon.Authenticated)
                {
                    throw new Exception($"Connect to {ip}:{port} completed but connection is not marked as 'Authenticated'?");
                }

                mConnection = rcon;

                AppendText($"Connected to {host}:{port}.");

                btnConnect.Enabled = false;
                btnDisconnect.Enabled = true;
                btnRefresh.Enabled = true;
                tbCommand.Enabled = true;
                tmrRefresh.Enabled = true;
                tmrLink.Enabled = true;

                SendCommand("status");
            }
            catch (Exception e)
            {
                // unlock UI
                btnConnect.Enabled = true;

                // connection failure
                MessageBox.Show(this, $"Failed to connect?{Environment.NewLine}{Environment.NewLine}{e.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void Disconnect()
        {
            btnConnect.Enabled = true;
            btnDisconnect.Enabled = false;
            btnRefresh.Enabled = false;
            tbCommand.Enabled = false;
            tmrRefresh.Enabled = false;
            tmrLink.Enabled = false;

            mCurrentStatus = null;
            lvPlayers.Items.Clear();
            lbTitle.Text = string.Empty;
            lbSummary.Text = string.Empty;
            SetLinkStatus(lbLinkIn, "In", false);

            try
            {
                mConnection?.Dispose();
            }
            catch { }
            mConnection = null;

            AppendText("Disconnected.");
        }

        private void Update(Status status)
        {
            // sanity check
            if (status == null)
            {
                return;
            }

            const int kPlayerSubItem = 1;
            const int kNetworkIdSubItem = 2;

            Func<Status_Player, Status_Player> namePatchFn = (o) =>
            {
                lock (sNameLookUpMutex)
                {
                    if (sNameLookUp.ContainsKey(o.Id))
                    {
                        // patch in Unicode name
                        o.Name = sNameLookUp[o.Id];
                    }
                    return o;
                }
            };

            var filter = tbFilter.Text.ToLower().Trim();

            int.TryParse(filter, out int filterAsInt);
            ulong.TryParse(filter, out ulong filterAsULong);

            // filter by user filter
            List<Status_Player> filteredList = string.IsNullOrEmpty(filter) ? status.PlayerList :
                status.PlayerList.Where(x => x.Name.ToLower().Contains(filter) || (filterAsInt > 0 && x.Id == filterAsInt) || (filterAsULong > 0 && x.NetworkId == filterAsULong)).ToList();

            var showBots = cbShowBots.Checked;
            var showSpectators = cbShowSpectators.Checked;

            // filter by type
            List<Status_Player> byTypeList = filteredList.Where(x => (x.Type == PlayerType.Bot && showBots) || (x.Type == PlayerType.Spectator && showSpectators) || x.Type == PlayerType.Human).ToList();
            
            // update players UI
            //lvPlayers.BeginUpdate();

            // find deletions
            var toDelete = new List<ListViewItem>();
            foreach (var obj in lvPlayers.Items)
            {
                var item = obj as ListViewItem;
                if (item.Tag != null && byTypeList.FirstOrDefault(x => x.Id == (item.Tag as Status_Player).Id) == null)
                {
                    // item no longer exists in player list
                    toDelete.Add(item);
                }
            }

            // apply deletions
            foreach (var item in toDelete)
            {
                lvPlayers.Items.Remove(item);
            }

            // apply additions/updates
            foreach (var rawPlayer in byTypeList)
            {
                var player = namePatchFn(rawPlayer);

                var key = player.Id.ToString();
                if (!lvPlayers.Items.ContainsKey(key))
                {
                    // add new player item
                    var lvi = lvPlayers.Items.Add(key, key, -1);
                    lvi.SubItems.Add(player.Name);
                    lvi.SubItems.Add(player.NetworkIdString);
                    lvi.Selected = mSelectedPlayerId == player.Id;
                    lvi.Tag = player;
                }
                else
                {
                    // modify existing player item (to take account of any name patching)
                    var existingLvi = lvPlayers.Items[key];
                    if (existingLvi.SubItems[kPlayerSubItem].Text != player.Name)
                    {
                        existingLvi.SubItems[kPlayerSubItem].Text = player.Name;
                    }
                }
            }

            //lvPlayers.EndUpdate();

            lbTitle.Text = $"{status.ServerName}";
            lbSummary.Text = $"{status.Humans} Players, {status.Bots} Bots && {status.Spectators} Spectators";

            mCurrentStatus = status;
        }

        private void AppendText(string text)
        {
            const int kMaxOutputLines = 256;

            tbOutput.AppendText(text + Environment.NewLine);
            var lines = tbOutput.Lines;

            if (lines.Length > kMaxOutputLines) 
            {
                // remove overflow lines
                var truncated = new string[kMaxOutputLines];
                Array.Copy(lines, lines.Length - kMaxOutputLines, truncated, 0, truncated.Length);
                tbOutput.Lines = truncated;

                // ensure scroll to bottom
                tbOutput.SelectionStart = tbOutput.TextLength;
                tbOutput.ScrollToCaret();
            }
        }

        private void SendCommand(string command)
        {
            command = command.ToLower().Trim();

            Cursor = Cursors.WaitCursor;

            var noResponseCommands = new List<string>() {
                "addbluforbots", "addbots", "addnamedbot", "addopforbots", "addteambots",  
                "removeallbots", "removebluforbots", "removeopforbots", "removeteambots",
                "say"
            };

            var maybeResponseCommands = new List<string>() {
                "servertravel"
            };

            if (mConnection != null && mConnection.Connected)
            {
                var parts = command.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length > 0)
                {
                    if (noResponseCommands.Contains(parts.First()))
                    {
                        // send command - expect no response
                        mConnection.SendCommandNoResponse(command, AppendText);
                    }
                    else if (maybeResponseCommands.Contains(parts.First()))
                    {
                        // send command - expect no response or maybe an error response
                        mConnection.SendCommandIgnoreTimeout(command, AppendText);
                    }
                    else
                    {
                        // send command - expect response
                        switch (parts.First())
                        {
                            case "status":
                                // restart the refresh timer
                                tmrRefresh.Stop();
                                tmrRefresh.Start();

                                // specific call to "status"
                                var status = mConnection.SendCommandParsed<Status>("status", AppendText);
                                Update(status);
                                break;
                            default:
                                // generic
                                mConnection.SendCommand(command, AppendText);
                                break;
                        }
                    }
                }
            }

            Cursor = Cursors.Default;
        }

        private void lvPlayers_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == lvwColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvwColumnSorter.Order == SortOrder.Ascending)
                {
                    lvwColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvwColumnSorter.SortColumn = e.Column;
                lvwColumnSorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            lvPlayers.Sort();
        }

        private void tbFilter_TextChanged(object sender, EventArgs e)
        {
            if (mCurrentStatus != null)
            {
                Update(mCurrentStatus);
            }
        }

        private void cmsPlayer_Opening(object sender, CancelEventArgs e)
        {
            var selectedItem = SelectedPlayerItem;
            if (selectedItem == null)
            {
                cmsPlayer.Tag = null;
                e.Cancel = true;
            }
            else
            {
                // dynamically enable/disable some features
                tsmiCopyNetworkId.Enabled = selectedItem.Type == PlayerType.Human;
                tsmiBan.Enabled = selectedItem.Type == PlayerType.Human;
                tsmiAddAdmin.Enabled = selectedItem.Type == PlayerType.Human;
                tsmiRemoveAdmin.Enabled = selectedItem.Type == PlayerType.Human;

                cmsPlayer.Tag = selectedItem;
            }
        }

        private void cbShowSpectators_CheckedChanged(object sender, EventArgs e)
        {
            Update(mCurrentStatus);
        }

        private void cbShowBots_CheckedChanged(object sender, EventArgs e)
        {
            Update(mCurrentStatus);
        }

        private void tsmiCopyNetworkId_Click(object sender, EventArgs e)
        {
            var player = cmsPlayer.Tag as Status_Player;
            if (player.Type == PlayerType.Human)
            {
                Clipboard.SetText(player.NetworkIdString);

                MessageBox.Show(this, $"Copied network identifier {player.NetworkIdString} for player '{player.Name}' to the Clipboard", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void tsmiKick_Click(object sender, EventArgs e)
        {
            var player = cmsPlayer.Tag as Status_Player;
            SendCommand($"kickid {player.Id}");
        }

        private void tsmiBan_Click(object sender, EventArgs e)
        {
            var player = cmsPlayer.Tag as Status_Player;
            SendCommand($"banid {player.Id}");
        }

        private void tsmiAddAdmin_Click(object sender, EventArgs e)
        {
            var player = cmsPlayer.Tag as Status_Player;
            SendCommand($"admin addid {player.Id}");
        }

        private void tsmiRemoveAdmin_Click(object sender, EventArgs e)
        {
            var player = cmsPlayer.Tag as Status_Player;
            SendCommand($"admin removeid {player.Id}");
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            SendCommand("status");
        }

        private void tmrRefresh_Tick(object sender, EventArgs e)
        {
            if (mConnection != null && mConnection.Connected)
            {
                SendCommand("status");
            }
        }

        private void tmrLink_Tick(object sender, EventArgs e)
        {
            if (!bwLink.IsBusy)
            {
                // trigger the background link task
                bwLink.RunWorkerAsync();
            }
        }

        private void bwLink_DoWork(object sender, DoWorkEventArgs e)
        {
            SauronLink.SauronImport();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
                tbHost.DataBindings.Add("Text", Properties.Settings.Default, "tbHost_Text", true, DataSourceUpdateMode.OnPropertyChanged);
                tbPort.DataBindings.Add("Text", Properties.Settings.Default, "tbPort_Text", true, DataSourceUpdateMode.OnPropertyChanged);
                tbPassword.DataBindings.Add("Text", Properties.Settings.Default, "tbPassword_Text", true, DataSourceUpdateMode.OnPropertyChanged);
            }
            catch { }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();
            Disconnect();
        }

        private void SetLinkStatus(Label label, string middleText, bool active)
        {
            label.BackColor = active ? Color.LightGreen : SystemColors.Control;
            label.Text = active ? $"Link {middleText} Active" : $"Link {middleText} Inactive";
            label.Enabled = active;
        }

        private Status_Player SelectedPlayerItem
        {
            get
            {
                return lvPlayers.SelectedItems.Count > 0 ? lvPlayers.SelectedItems[0].Tag as Status_Player : null;
            }
        }

        private void lvPlayers_SelectedIndexChanged(object sender, EventArgs e)
        {
            // record latest selection data
            if (SelectedPlayerItem != null)
            {
                mSelectedPlayerId = SelectedPlayerItem.Id;
                mSelectedPlayerAt = DateTime.UtcNow;
            }
        }

        private AutoCompleteStringCollection ReadAutoCompleteFile()
        {
            var rv = new AutoCompleteStringCollection();
            var autoCompleteFile = Properties.Resources.autocomplete;
            StringReader autoCompleteStringReader = new StringReader(autoCompleteFile);
            string line;
            do
            {
                line = autoCompleteStringReader.ReadLine();
                if (line != null)
                {
                    rv.Add(line);
                }
            }
            while (line != null);

            return rv;
        }

        // private variables
        private CoreRCON.RCON mConnection;
        private ListViewColumnSorter lvwColumnSorter;
        private Status mCurrentStatus;
        private int mSelectedPlayerId;
        private DateTime mSelectedPlayerAt;

        // private static variables
        public static object sNameLookUpMutex = new object();
        public static Dictionary<int, string> sNameLookUp = new Dictionary<int, string>();
    }
}
