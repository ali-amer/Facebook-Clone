using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dbProj4
{
    public partial class fbNewsfeed : UserControl
    {
        public fbNewsfeed()
        {
            InitializeComponent();
        }

        public Image PostImage
        {
            get { return pictureBoxPosts.Image; }
            set { pictureBoxPosts.Image = value; }
        }

        public Image ProfilePic
        {
            get { return pictureBoxProfile.Image; }
            set { pictureBoxProfile.Image = value; }
        }
        public string UserName
        {
            get { return labelUserName.Text; }
            set { labelUserName.Text = value; }
        }
        public string PostTime
        {
            get { return labelTime.Text; }
            set { labelTime.Text = value; }
        }
    }
}
