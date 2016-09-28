using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class ListModel
    {
        private int _fid;
        private string _filename;
        private string _thumbname;
        private string _filesize;
        private string _uploadtime;
        private int _download;
        private float _amount;

        public int Fid
        {
            get { return _fid; }
            set { _fid = value; }
        }

        public string FileName
        {
            get { return _filename; }
            set { _filename = value; }
        }

        public string ThumbName
        {
            get { return _thumbname; }
            set { _thumbname = value; }
        }

        public string FileSize
        {
            get { return _filesize; }
            set { _filesize = value; }
        }

        public string UploadTime
        {
            get { return _uploadtime; }
            set { _uploadtime = value; }
        }

        public int DownLoad
        {
            get { return _download; }
            set { _download = value; }
        }

        public float Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
    }
}
