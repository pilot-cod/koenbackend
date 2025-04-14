using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

[ApiController]
[Route("api/SanPhams")]
public class SanPhamsController : ControllerBase
{
    private static readonly List<SanPham> _sanPhams = new List<SanPham>()
    {
        new SanPham { id = 1, tenSanPham = "Sản phẩm A", gia = 99.99, imageUrl = "/images/sp1.jpg", maSanPham = "SP001", soLuong = 100, danhMuc = "Điện tử" },
        new SanPham { id = 2, tenSanPham = "Sản phẩm B", gia = 49.50, imageUrl = "/images/sp2.jpg", maSanPham = "SP002", soLuong = 50, danhMuc = "Thời trang" },
        new SanPham { id = 3, tenSanPham = "Sản phẩm C", gia = 120.00, imageUrl = "/images/sp3.jpg", maSanPham = "SP003", soLuong = 200, danhMuc = "Gia dụng" }
        // Thêm dữ liệu sản phẩm mẫu khác nếu cần
    };

    [HttpGet]
    public ActionResult<IEnumerable<SanPham>> Get()
    {
        return Ok(_sanPhams);
    }

    [HttpGet("{id}")]
    public ActionResult<SanPham> Get(int id)
    {
        var sanPham = _sanPhams.FirstOrDefault(p => p.id == id);
        if (sanPham == null)
        {
            return NotFound();
        }
        return Ok(sanPham);
    }

    [HttpPost]
    public ActionResult<SanPham> Post(SanPham sanPhamMoi)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        sanPhamMoi.id = _sanPhams.Any() ? _sanPhams.Max(p => p.id) + 1 : 1;
        _sanPhams.Add(sanPhamMoi);

        // Thông báo cho client biết sản phẩm mới đã được tạo và trả về thông tin sản phẩm
        return CreatedAtAction(nameof(Get), new { id = sanPhamMoi.id }, sanPhamMoi);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, SanPham sanPhamCapNhat)
    {
        if (id != sanPhamCapNhat.id)
        {
            return BadRequest("ID sản phẩm không khớp.");
        }

        var existingSanPham = _sanPhams.FirstOrDefault(p => p.id == id);
        if (existingSanPham == null)
        {
            return NotFound();
        }

        // Cập nhật các thuộc tính
        existingSanPham.tenSanPham = sanPhamCapNhat.tenSanPham;
        existingSanPham.moTa = sanPhamCapNhat.moTa;
        existingSanPham.gia = sanPhamCapNhat.gia;
        existingSanPham.imageUrl = sanPhamCapNhat.imageUrl;
        existingSanPham.maSanPham = sanPhamCapNhat.maSanPham;
        existingSanPham.soLuong = sanPhamCapNhat.soLuong;
        existingSanPham.danhMuc = sanPhamCapNhat.danhMuc;
        existingSanPham.nhaSanXuat = sanPhamCapNhat.nhaSanXuat;
        existingSanPham.ngayTao = sanPhamCapNhat.ngayTao; // Cân nhắc cách xử lý thuộc tính này
        existingSanPham.ngayCapNhat = DateTime.Now; // Cập nhật ngày cập nhật

        return NoContent(); // Trả về 204 No Content khi cập nhật thành công
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var sanPham = _sanPhams.FirstOrDefault(p => p.id == id);
        if (sanPham == null)
        {
            return NotFound();
        }

        _sanPhams.Remove(sanPham);
        return NoContent(); // Trả về 204 No Content khi xóa thành công
    }
}

public class SanPham
{
    public int id { get; set; }
    public string tenSanPham { get; set; }
    public string? moTa { get; set; }
    public double gia { get; set; }
    public string imageUrl { get; set; }
    public string maSanPham { get; set; }
    public int soLuong { get; set; }
    public string? danhMuc { get; set; }
    public string? nhaSanXuat { get; set; }
    public DateTime? ngayTao { get; set; }
    public DateTime? ngayCapNhat { get; set; }
    public double? rating { get; set; }
    // Thêm các thuộc tính khác nếu cần
}