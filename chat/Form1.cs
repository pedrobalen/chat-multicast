using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace chat
{
    public partial class Form1 : Form
    {
        private UdpCommunicator udpCommunicator;
        private List<string> messagesList = new List<string>();
        private Thread receiverThread;
        private bool isRunning = true;
        private Dictionary<string, PrivateChatForm> privateChatForms = new Dictionary<string, PrivateChatForm>();


        public Form1()
        {
            InitializeComponent();
            ip_field.Text = "239.1.2.3";
            port_field.Text = "3456";
            SetupPrivateChat();

        }

        private void SetupPrivateChat()
        {
            chat_box.DetectUrls = false;
            chat_box.MouseClick += Chat_box_MouseClick;
        }

        private void Chat_box_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                int charIndex = chat_box.GetCharIndexFromPosition(e.Location);
                int lineIndex = chat_box.GetLineFromCharIndex(charIndex);

                if (lineIndex < 0 || lineIndex >= chat_box.Lines.Length)
                    return;

                string line = chat_box.Lines[lineIndex];

                string[] parts = line.Split(new[] { "] ", ": " }, StringSplitOptions.None);
                if (parts.Length >= 2)
                {
                    string clickedUsername = parts[1].Trim();

                    if (clickedUsername != username_field.Text && !line.Contains("joined the room") && !line.Contains("left the room"))
                    {
                        OpenPrivateChat(clickedUsername);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening private chat: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenPrivateChat(string targetUser)
        {
            if (!privateChatForms.ContainsKey(targetUser))
            {
                var privateChat = new PrivateChatForm(
                    targetUser,
                    username_field.Text,
                    ip_field.Text,
                    int.Parse(port_field.Text)
                );

                privateChatForms[targetUser] = privateChat;
                privateChat.FormClosed += (s, e) => privateChatForms.Remove(targetUser);
                privateChat.Show();
            }
            else
            {
                privateChatForms[targetUser].BringToFront();
            }
        }

        private void button1_Click(object sender, EventArgs e) // Connect
        {
            try
            {
                if (string.IsNullOrEmpty(username_field.Text))
                {
                    MessageBox.Show("Please enter your nickname");
                    return;
                }

                udpCommunicator = new UdpCommunicator(ip_field.Text, int.Parse(port_field.Text));

                receiverThread = new Thread(new ThreadStart(ReceiveMessages));
                receiverThread.IsBackground = true;
                receiverThread.Start();

                MessageBox.Show("Successfully connected!");
                connect_button.Enabled = false;
                username_field.Enabled = false;
                ip_field.Enabled = false;
                port_field.Enabled = false;

                udpCommunicator.SendMessage($"{username_field.Text} joined the room");
                sendmessagetext_field.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void ReceiveMessages()
        {
            while (isRunning)
            {
                try
                {
                    string message = udpCommunicator.ReceiveMessage();

                    if (message.StartsWith("PRIVATE|||"))
                    {
                        HandlePrivateMessage(message);
                        continue;
                    }

                    Invoke((MethodInvoker)delegate
                    {
                        messagesList.Add(message);
                        UpdateMessageDisplay();
                    });
                }
                catch (Exception ex)
                {
                    if (isRunning)
                    {
                        Invoke((MethodInvoker)delegate
                        {
                            MessageBox.Show(ex.Message);
                        });
                    }
                }
            }
        }

        private void HandlePrivateMessage(string message)
        {
            string[] parts = message.Split(new[] { "|||" }, StringSplitOptions.None);
            if (parts.Length == 4 && parts[0] == "PRIVATE")
            {
                string sender = parts[1];
                string recipient = parts[2];

                // Only handle messages where we are the recipient
                if (recipient == username_field.Text)
                {
                    Invoke((MethodInvoker)delegate
                    {
                        // Open private chat window if it doesn't exist
                        if (!privateChatForms.ContainsKey(sender))
                        {
                            OpenPrivateChat(sender);
                        }
                    });
                }
            }
        }

        private void UpdateMessageDisplay()
        {
            chat_box.Clear();

            foreach (var message in messagesList)
            {
                string[] parts = message.Split(new[] { ": " }, 2, StringSplitOptions.None); 
                if (parts.Length == 2)
                {
                    string username = parts[0];
                    string content = parts[1];


                    chat_box.SelectionFont = new Font(chat_box.Font, FontStyle.Regular);
                    chat_box.AppendText($"[{DateTime.Now:HH:mm}] ");


                    chat_box.SelectionFont = new Font(chat_box.Font, FontStyle.Bold);
                    chat_box.AppendText(username + ": ");


                    chat_box.SelectionFont = new Font(chat_box.Font, FontStyle.Regular);
                    chat_box.AppendText(content + Environment.NewLine);
                }
                else
                {

                    chat_box.SelectionFont = new Font(chat_box.Font, FontStyle.Regular);
                    chat_box.AppendText(message + Environment.NewLine);
                }
            }


            chat_box.SelectionStart = chat_box.Text.Length;
            chat_box.ScrollToCaret();
        }



        private void button2_Click(object sender, EventArgs e) // Send message
        {
            if (udpCommunicator != null)
            {
                string message = $"{username_field.Text}: {sendmessagetext_field.Text}";
                udpCommunicator.SendMessage(message);
                sendmessagetext_field.Text = "";
            }
            else
            {
                MessageBox.Show("Connect before sending messages.");
            }
        }

        private void exit_button_Click(object sender, EventArgs e) // Quit
        {
            ExitSystem();
        }

        private void ExitSystem()
        {
            foreach (var privateChat in privateChatForms.Values)
            {
                privateChat.Close();
            }
            privateChatForms.Clear();

            if (udpCommunicator != null)
            {
                udpCommunicator.LeaveGroup($"{username_field.Text} left the room");
                udpCommunicator.Dispose();
            }
            Application.Exit();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            isRunning = false;
            udpCommunicator?.Dispose();
            base.OnFormClosing(e);
        }

        private void sendmessagetext_field_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;

                if (udpCommunicator != null)
                {
                    string message = $"{username_field.Text}: {sendmessagetext_field.Text}";
                    udpCommunicator.SendMessage(message);
                    sendmessagetext_field.Text = "";
                }
                else
                {
                    MessageBox.Show("Connect before sending messages.");
                }
            }
        }

    }
}