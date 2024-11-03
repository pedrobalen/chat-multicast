namespace chat
{
    partial class PrivateChatForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            chatBox = new RichTextBox();
            messageBox = new TextBox();
            sendButton = new Button();
            SuspendLayout();
            // 
            // chatBox
            // 
            chatBox.Location = new Point(12, 12);
            chatBox.Name = "chatBox";
            chatBox.Size = new Size(472, 447);
            chatBox.TabIndex = 0;
            chatBox.Text = "";
            // 
            // messageBox
            // 
            messageBox.Location = new Point(12, 465);
            messageBox.Name = "messageBox";
            messageBox.Size = new Size(391, 23);
            messageBox.TabIndex = 1;
            // 
            // sendButton
            // 
            sendButton.Location = new Point(409, 467);
            sendButton.Name = "sendButton";
            sendButton.Size = new Size(75, 23);
            sendButton.TabIndex = 2;
            sendButton.Text = "Send";
            sendButton.UseVisualStyleBackColor = true;
            sendButton.Click += sendButton_Click_1;
            // 
            // PrivateChatForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(496, 502);
            Controls.Add(sendButton);
            Controls.Add(messageBox);
            Controls.Add(chatBox);
            Name = "PrivateChatForm";
            Text = "PrivateChatForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RichTextBox chatBox;
    }
}