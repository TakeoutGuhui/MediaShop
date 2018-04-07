using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MediaShop.Models
{
    class Printer
    {
        private string[] linesToPrint;
        private int lineIndex = 0;
        private Font printFont;

        private void PrintPage(object sender, PrintPageEventArgs e)
        {
            //string[] delim = { Environment.NewLine, "\n" }; // "\n" added in case you manually appended a newline
            //string[] lines = ShoppingCart.ToString().Split(delim, StringSplitOptions.None);
            float linesPerPage = 0;
            float yPos = 0;
            int count = 0;
            float leftMargin = e.MarginBounds.Left;
            float topMargin = e.MarginBounds.Top;
            string line = null;

            // Calculate the number of lines per page.
            linesPerPage = e.MarginBounds.Height /
               printFont.GetHeight(e.Graphics);

            // Print each line of the file.
            while (count < linesPerPage &&
               ((line = linesToPrint[lineIndex++]) != null))
            {
                yPos = topMargin + (count *
                   printFont.GetHeight(e.Graphics));
                e.Graphics.DrawString(line, printFont, Brushes.Black,
                   leftMargin, yPos, new StringFormat());
                count++;
            }

            // If more lines exist, print another page.
            if (line != null)
                e.HasMorePages = true;
            else
                e.HasMorePages = false;
        }

        public void bla(String printText) {
            string[] delim = { Environment.NewLine, "\n" }; // "\n" added in case you manually appended a newline
            linesToPrint = printText.Split(delim, StringSplitOptions.None);



            PrintDialog dialog = new PrintDialog();
            printFont = new Font("Arial", 10);
            PrintDocument document = new PrintDocument();
            document.PrintPage += PrintPage;

            if (dialog.ShowDialog() == true)
            {
                document.Print();
            }
        }
    }
}
