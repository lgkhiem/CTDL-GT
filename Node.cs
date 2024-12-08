using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagerWinform
{
    public class Node
    {
        public int ID { get; set; } // ID nhân viên
        public string Name { get; set; } // Tên nhân viên
        public string Position { get; set; } // Chức vụ
        public DateTime Timestamp { get; set; } // Ngày thêm nhân viên
        public Node LeftNode { get; set; } // Nút con bên trái
        public Node RightNode { get; set; } // Nút con bên phải

        public Node(int id, string name, string position, DateTime timestamp)
        {
            ID = id;
            Name = name;
            Position = position;
            Timestamp = timestamp;
            LeftNode = null;
            RightNode = null;
        }
    }


    }

