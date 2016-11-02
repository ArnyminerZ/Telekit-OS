using HtmlAgilityPack;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace TelekitOS_WindowsPreview
{
    public partial class Diccionario : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;
    
        public Diccionario()
        {
            InitializeComponent();
            // Initialize MaterialSkinManager
            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.LightGreen900,
                                                              Primary.Green900, 
                                                              Primary.BlueGrey500, 
                                                              Accent.LightBlue200, 
                                                              TextShade.WHITE);
        }

        private void Diccionario_Load(object sender, EventArgs e)
        {
            
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            string domainParsing = null;
            string searchTerm = materialSingleLineTextField1.Text;
            if(materialTabControl1.SelectedTab.Text.Equals("Español", StringComparison.CurrentCultureIgnoreCase))
            {
                domainParsing = "http://www.wordreference.com/definicion/%s%";
                MessageBox.Show("Searching for " + searchTerm + " in wordreference.com");
            }else if (materialTabControl1.SelectedTab.Text.Equals("Català", StringComparison.CurrentCultureIgnoreCase))
            {
                domainParsing = "http://www.wordreference.com/definicio/%s%";
                MessageBox.Show("Searching for " + searchTerm + " in wordreference.com");
            }
            string url = domainParsing.Replace("%s%", searchTerm);
            MessageBox.Show("Search url is " + url);
            string webRead = getWebCode(url).Replace("doctype", "DOCTYPE");
            File.WriteAllText("dict_response.xml", webRead);

            materialListView1.Columns.Add(searchTerm);

            List<string> nodeGet = selectNodesFromWebByClass(webRead, "div", "trans clickable");
            foreach(string node in nodeGet)
            {
                materialListView1.Items.Add(node);
            }
        }

        public string getWebCode(string web)
        {
            string urlAddress = "http://google.com";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;

                if (response.CharacterSet == null)
                {
                    readStream = new StreamReader(receiveStream);
                }
                else
                {
                    readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                }

                string data = readStream.ReadToEnd();

                response.Close();
                readStream.Close();

                return data;
            }
            else
                return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="htmlCode">The html code</param>
        /// <param name="nodeToSelect">Must be written "//nodeName"</param>
        /// <param name="classValue">The class value for selecting. Indiferent capital letters</param>
        /// <returns></returns>
        public List<string> selectNodesFromWebByClass(string htmlCode, string nodeToSelect, string classValue)
        {
            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
            
            if (document == null)
            {
                MessageBox.Show("Error! \"document\" is null");
            }

            string htmlString = htmlCode;
            document.LoadHtml(htmlString);
            HtmlNodeCollection collection = document.DocumentNode.SelectNodes(nodeToSelect);

            if(collection == null)
            {
                MessageBox.Show("Error! \"collection\" is null");
            }

            List<string> foundNodes = new List<string>();
            foreach (HtmlNode link in collection)
            {
                string target = link.Attributes["class"].Value;
                if(target.Equals(classValue, StringComparison.CurrentCultureIgnoreCase))
                {
                    foundNodes.Add(target);
                }
            }
            return foundNodes;
        }
    }
}
