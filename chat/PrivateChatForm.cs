using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace chat
{
    public partial class PrivateChatForm : Form
    {
        private TextBox messageBox;
        private Button sendButton;
        private readonly string targetUser;
        private readonly string currentUser;
        private readonly UdpCommunicator udpCommunicator;
        private readonly List<string> messageHistory;
        private bool isRunning = true;
        private System.Threading.Thread receiverThread;

        public PrivateChatForm(string targetUser, string currentUser, string ipAddress, int port)
        {
            this.targetUser = targetUser;
            this.currentUser = currentUser;
            this.messageHistory = new List<string>();

            this.udpCommunicator = new UdpCommunicator(ipAddress, port + 1);

            InitializeComponents();
            StartReceiving();
        }

        private void InitializeComponents()
        {
            this.Text = $"Private Chat with {targetUser}";
            this.Size = new Size(400, 500);
            this.FormClosing += PrivateChatForm_FormClosing;


            chatBox = new RichTextBox
            {
                Location = new Point(12, 12),
                Size = new Size(360, 380),
                ReadOnly = true,
                BackColor = Color.White
            };


            messageBox = new TextBox
            {
                Location = new Point(12, 400),
                Size = new Size(280, 40),
                Multiline = false
            };
            messageBox.KeyPress += MessageBox_KeyPress;


            sendButton = new Button
            {
                Location = new Point(298, 400),
                Size = new Size(75, 23),
                Text = "Send",
            };
            sendButton.Click += SendButton_Click;


            this.Controls.AddRange(new Control[] { chatBox, messageBox, sendButton });
        }

        private void StartReceiving()
        {
            receiverThread = new System.Threading.Thread(new System.Threading.ThreadStart(ReceiveMessages))
            {
                IsBackground = true
            };
            receiverThread.Start();
        }

        private void ReceiveMessages()
        {
            while (isRunning)
            {
                try
                {
                    string message = udpCommunicator.ReceiveMessage();
                    string[] parts = message.Split(new[] { "|||" }, StringSplitOptions.None);

                    if (parts.Length == 4 && parts[0] == "PRIVATE")
                    {
                        string sender = parts[1];
                        string recipient = parts[2];
                        string content = parts[3];

                        if ((sender == currentUser && recipient == targetUser) ||
                            (sender == targetUser && recipient == currentUser))
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                AppendMessage(sender, content);
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (isRunning)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        });
                    }
                }
            }
        }

        private void SendPrivateMessage(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                return;

            try
            {
                string formattedMessage = $"PRIVATE|||{currentUser}|||{targetUser}|||{message}";
                udpCommunicator.SendMessage(formattedMessage);
                messageBox.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error sending message: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AppendMessage(string sender, string message)
        {
            chatBox.SelectionStart = chatBox.TextLength;
            chatBox.SelectionLength = 0;

            chatBox.SelectionColor = Color.Gray;
            chatBox.AppendText($"[{DateTime.Now:HH:mm}] ");

            chatBox.SelectionColor = Color.Blue;
            chatBox.SelectionFont = new Font(chatBox.Font, FontStyle.Bold);
            chatBox.AppendText($"{sender}: ");

            chatBox.SelectionColor = Color.Black;
            chatBox.SelectionFont = new Font(chatBox.Font, FontStyle.Regular);
            chatBox.AppendText($"{message}{Environment.NewLine}");

            chatBox.SelectionStart = chatBox.Text.Length;
            chatBox.ScrollToCaret();
        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            SendPrivateMessage(messageBox.Text);
        }

        private void MessageBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                SendPrivateMessage(messageBox.Text);
            }
        }

        private void PrivateChatForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            isRunning = false;
            udpCommunicator?.Dispose();
        }

        private void sendButton_Click_1(object sender, EventArgs e)
        {

        }
    }
}