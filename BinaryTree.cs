using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagerWinform
{
    public class BinaryTree
    {
        public Node Root { get; set; }

        public BinaryTree()
        {
            Root = null;
        }

        // Thêm nhân viên
        public bool ThemNhanVien(int id, string name, string position, DateTime timestamp)
        {
            Root = Insert(Root, id, name, position, timestamp);
            return true;
        }

        private Node Insert(Node root, int id, string name, string position, DateTime timestamp)
        {
            if (root == null)
                return new Node(id, name, position, timestamp);

            if (timestamp < root.Timestamp)
                root.LeftNode = Insert(root.LeftNode, id, name, position, timestamp);
            else if (timestamp > root.Timestamp)
                root.RightNode = Insert(root.RightNode, id, name, position, timestamp);

            return root;
        }

        // Xóa nhân viên
        public bool XoaNhanVien(int id)
        {
            Root = Delete(Root, id);
            return true;
        }

        private Node Delete(Node root, int id)
        {
            if (root == null) return null;

            if (id < root.ID)
                root.LeftNode = Delete(root.LeftNode, id);
            else if (id > root.ID)
                root.RightNode = Delete(root.RightNode, id);
            else
            {
                if (root.LeftNode == null) return root.RightNode;
                if (root.RightNode == null) return root.LeftNode;

                root.ID = MinValue(root.RightNode).ID;
                root.RightNode = Delete(root.RightNode, root.ID);
            }
            return root;
        }

        private Node MinValue(Node root)
        {
            Node current = root;
            while (current.LeftNode != null)
                current = current.LeftNode;
            return current;
        }

        // Cập nhật thông tin nhân viên
        public bool CapNhatNhanVien(int id, string newName, string newPosition)
        {
            var node = TimKiemTheoID(id);
            if (node != null)
            {
                node.Name = newName;
                node.Position = newPosition;
                return true;
            }
            return false;
        }

        // Tìm kiếm nhân viên theo ID
        public Node TimKiemTheoID(int id)
        {
            return SearchByID(Root, id);
        }

        private Node SearchByID(Node root, int id)
        {
            if (root == null || root.ID == id)
                return root;

            if (id < root.ID)
                return SearchByID(root.LeftNode, id);

            return SearchByID(root.RightNode, id);
        }

        // Tìm kiếm theo tên
        public List<Node> TimKiemTheoTen(string name)
        {
            List<Node> results = new List<Node>();
            SearchByName(Root, name, results);
            return results;
        }

        private void SearchByName(Node root, string name, List<Node> results)
        {
            if (root == null) return;

            if (root.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                results.Add(root);

            SearchByName(root.LeftNode, name, results);
            SearchByName(root.RightNode, name, results);
        }

        // Tìm kiếm trong khoảng thời gian
        public List<Node> TimKiemTheoKhoangThoiGian(DateTime start, DateTime end)
        {
            List<Node> results = new List<Node>();
            SearchByTimeRange(Root, start, end, results);
            return results;
        }

        private void SearchByTimeRange(Node root, DateTime start, DateTime end, List<Node> results)
        {
            if (root == null) return;

            if (root.Timestamp >= start && root.Timestamp <= end)
                results.Add(root);

            if (root.Timestamp > start)
                SearchByTimeRange(root.LeftNode, start, end, results);

            if (root.Timestamp < end)
                SearchByTimeRange(root.RightNode, start, end, results);
        }

        // Đếm tổng số nhân viên
        public int DemTongSoNhanVien()
        {
            return CountNodes(Root);
        }

        private int CountNodes(Node root)
        {
            if (root == null)
                return 0;

            return 1 + CountNodes(root.LeftNode) + CountNodes(root.RightNode);
        }
    }

}
