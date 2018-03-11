using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using ACBr.Net.Core.Extensions;

namespace ACBr.Net.CTe.Demo
{
    public static class UIExtensions
    {
        public static void LoadXml(this WebBrowser browser, string xml)
        {
            if (xml == null)
                return;

            var path = Path.GetTempPath();
            var fileName = Guid.NewGuid() + ".xml";
            var fullFileName = Path.Combine(path, fileName);
            var xmlDoc = File.Exists(xml) ? XDocument.Load(xml) : XDocument.Parse(xml);

            xmlDoc.Save(fullFileName);
            browser.Navigate(fullFileName);
        }

        public static void EnumDataSource<T>(this ComboBox cmb) where T : struct
        {
            cmb.DataSource = (from T value in Enum.GetValues(typeof(T)) select new ItemData<T>(value))
                .OrderBy(x => x.Description).ToArray();
        }

        public static void EnumDataSource<T>(this ComboBox cmb, T valorPadrao) where T : struct
        {
            var dataSource = (from T value in Enum.GetValues(typeof(T)) select new ItemData<T>(value))
                .OrderBy(x => x.Description).ToArray();
            cmb.DataSource = dataSource;
            cmb.SelectedItem = dataSource.SingleOrDefault(x => x.Content.Equals(valorPadrao));
        }

        public static void EnumDataSource<T>(this ComboBox cmb, T valorPadrao, params T[] excluded) where T : struct
        {
            var dataSource = (from T value in Enum.GetValues(typeof(T)) where !value.IsIn(excluded) select new ItemData<T>(value))
                .OrderBy(x => x.Description).ToArray();
            cmb.DataSource = dataSource;
            cmb.SelectedItem = dataSource.SingleOrDefault(x => x.Content.Equals(valorPadrao));
        }

        public static T GetSelectedValue<T>(this ComboBox cmb) where T : struct
        {
            return ((ItemData<T>)cmb.SelectedItem).Content;
        }

        public static void SetSelectedValue<T>(this ComboBox cmb, T value) where T : struct
        {
            var values = (ItemData<T>[])cmb.DataSource;
            cmb.SelectedItem = values.SingleOrDefault(x => x.Content.Equals(value));
        }

        public static void AppendLine(this RichTextBox richText, string line)
        {
            richText.AppendText(line + Environment.NewLine);
        }
    }
}