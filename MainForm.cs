using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using System.Xml.Linq;

namespace FileManagerWinform
{
    public partial class MainForm : Form
    {
        private BinaryTree tree;

        public MainForm()
        {
            InitializeComponent();
            tree = new BinaryTree();
        }

        // Nút Thêm hồ sơ
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                int data = int.Parse(txtData.Text); // Lấy giá trị từ TextBox
                string name = txtName.Text.Trim(); // Lấy tên từ TextBox
                DateTime timestamp = dtpTimestamp.Value; // Lấy thời gian từ DateTimePicker

                if (string.IsNullOrEmpty(name))
                {
                    MessageBox.Show("Vui lòng nhập tên hồ sơ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                tree.Insert(data, name, timestamp);
                MessageBox.Show("Thêm hồ sơ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                UpdateGridView();
                ClearInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Nút Xóa hồ sơ
        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtName.Text.Trim(); // Lấy tên từ TextBox

                if (string.IsNullOrEmpty(name))
                {
                    MessageBox.Show("Vui lòng nhập tên hồ sơ để xóa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Node foundNode = tree.SearchByName(name);
                if (foundNode != null)
                {
                    tree.Delete(name);
                    MessageBox.Show("Xóa hồ sơ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    UpdateGridView();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy hồ sơ để xóa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                ClearInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Nút Tìm kiếm
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                if (rbName.Checked) // Tìm kiếm theo tên
                {
                    string name = txtSearch.Text.Trim();

                    if (string.IsNullOrEmpty(name))
                    {
                        MessageBox.Show("Vui lòng nhập tên cần tìm!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    Node result = tree.SearchByName(name);
                    if (result != null)
                    {
                        MessageBox.Show($"Hồ sơ: {result.Name}, Ngày lưu: {result.Timestamp}", "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy hồ sơ!", "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else if (rbTime.Checked) // Tìm kiếm theo khoảng thời gian
                {
                    DateTime start = dtpStart.Value;
                    DateTime end = dtpEnd.Value;

                    if (start > end)
                    {
                        MessageBox.Show("Ngày bắt đầu không được lớn hơn ngày kết thúc!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    List<Node> results = tree.SearchByTimeRange(start, end);
                    if (results.Count > 0)
                    {
                        dgvData.Rows.Clear();
                        foreach (var node in results)
                        {
                            dgvData.Rows.Add(node.Data, node.Name, node.Timestamp);
                        }
                        MessageBox.Show($"Tìm thấy {results.Count} hồ sơ trong khoảng thời gian đã chọn.", "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy hồ sơ nào trong khoảng thời gian này!", "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn loại tìm kiếm (Theo tên hoặc Theo khoảng thời gian)!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Nút Tải lại
        private void btnTaiLai_Click(object sender, EventArgs e)
        {
            UpdateGridView();
        }

        // Hàm cập nhật DataGridView
        private void UpdateGridView()
        {
            dgvData.Rows.Clear();
            AddNodesToGrid(tree.Root);
        }

        private void AddNodesToGrid(Node root)
        {
            if (root != null)
            {
                AddNodesToGrid(root.LeftNode);
                dgvData.Rows.Add(root.Data, root.Name, root.Timestamp);
                AddNodesToGrid(root.RightNode);
            }
        }

        // Hàm xóa nội dung nhập liệu
        private void ClearInputs()
        {
            txtData.Clear();
            txtName.Clear();
            txtSearch.Clear();
            dtpTimestamp.Value = DateTime.Now;
            dtpStart.Value = DateTime.Now;
            dtpEnd.Value = DateTime.Now;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
    }
}
