using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace oCryptoBruteForce
{
    public partial class WorkerViewForm : Form
    {
        public WorkerViewForm(DelegateObject input)
        {
            InitializeComponent();
            //=====================================================
            Text = "Worker Details - Worker Id: " + input.WorkerId;
            fileNameLabel.Text = input.FileName;
            fileLocationLabel.Text = input.FileLocation;
            if(!string.IsNullOrEmpty(input.PossibleChecksumFileName))
            {
                possibleChecksumFileLabel.Text = input.PossibleChecksumFileName;
                possibleChecksumFileLocation.Text = input.PossibleChecksumFileLocation;
            }
            if (input.ExhaustiveSearch) exhausiveSearchLabel.Text = "Yes";
            if (input.UseTPL) taskParallelismLabel.Text = "Yes";
            if (input.ConvertFromBase64String) convertFromBase64StringLabel.Text = "Yes";
            startFromPositionLabel.Text = input.StartSearch.ToString();
            stopAtPositionLabel.Text = input.StopSearchAt.ToString();
            startGenerationFromLabel.Text = input.StartGeneratedChecksumFrom.ToString();
            switch (input.SearchType)
            {
                case SearchTypeEnum.LazyGenerateLazySearch:
                    lazyGenerateLabel.Text = "Yes";
                    lazySearchLabel.Text = "Yes";
                    enableByteSkippingLabel.Text = "Yes";
                    if(input.Checksum != "")
                        skipBytesLabel.Text = input.Checksum.Length.ToString();
                    break;
                case SearchTypeEnum.NotLazyGenerateNotLazySearch:
                    skipBytesLabel.Text = "1";
                    break;
                case SearchTypeEnum.LazyGenerateNotLazySearch:
                    lazyGenerateLabel.Text = "Yes";
                    enableByteSkippingLabel.Text = "Yes";
                    skipBytesLabel.Text = input.SkipSearchBytesBy.ToString();
                    break;
                case SearchTypeEnum.NotLazyGenerateLazySearch:
                    lazySearchLabel.Text = "Yes";
                    enableByteSkippingLabel.Text = "Yes";
                    if(input.Checksum != "")
                        skipBytesLabel.Text = input.Checksum.Length.ToString();
                    break;
            }
        }
    }
}
