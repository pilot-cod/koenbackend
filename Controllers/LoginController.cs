using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// Namespace cho controller (tùy chọn)
// namespace YourBackendName.Controllers
// {
    [ApiController]
    [Route("api/Login")]
    public class LoginController : ControllerBase
    {
        // Giả sử bạn có một service để xử lý logic xác thực
        // private readonly IAuthService _authService;

        // public LoginController(IAuthService authService)
        // {
        //     _authService = authService;
        // }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Gọi service xác thực để kiểm tra thông tin đăng nhập
            // var authResult = await _authService.Authenticate(request.UsernameOrEmail, request.Password);

            // Mô phỏng xác thực thành công
            if (request.UsernameOrEmail == "testuser" && request.Password == "password")
            {
                // Tạo token (sử dụng JWT hoặc cơ chế khác)
                var token = GenerateJwtToken(request.UsernameOrEmail); // Hàm này bạn cần triển khai

                return Ok(new LoginResponse { Token = token });
            }
            else if (request.UsernameOrEmail == "email@example.com" && request.Password == "secret")
            {
                var token = GenerateJwtToken(request.UsernameOrEmail);
                return Ok(new LoginResponse { Token = token });
            }
            else
            {
                return Unauthorized("Thông tin đăng nhập không hợp lệ.");
            }
        }

        // Hàm giả định tạo JWT token (bạn cần triển khai logic tạo token thực tế)
        private string GenerateJwtToken(string usernameOrEmail)
        {
            // Trong ứng dụng thực tế, bạn sẽ sử dụng thư viện JWT
            // để tạo token dựa trên thông tin người dùng, claims, thời gian hết hạn, v.v.
            return $"fake-jwt-token-for-{usernameOrEmail}";
        }
    }

    public class LoginRequest
    {
        public string UsernameOrEmail { get; set; }
        public string Password { get; set; }
    }

    public class LoginResponse
    {
        public string Token { get; set; }
        // Bạn có thể thêm các trường khác từ phản hồi API của bạn
        // public int UserId { get; set; }
        // public string Message { get; set; }
    }
// }