using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

// Namespace cho controller (tùy chọn)
// namespace YourBackendName.Controllers
// {
    [ApiController]
    [Route("api/Register")]
    public class RegisterController : ControllerBase
    {
        // Giả sử bạn có một service hoặc repository để xử lý logic đăng ký
        // private readonly IUserService _userService;

        // public RegisterController(IUserService userService)
        // {
        //     _userService = userService;
        // }

        [HttpPost("checkUsernameAvailability")]
        public async Task<IActionResult> CheckUsernameAvailability([FromBody] CheckUsernameRequest request)
        {
            if (string.IsNullOrEmpty(request?.Username))
            {
                return BadRequest("Tên đăng nhập không được để trống.");
            }

            // Gọi service/repository để kiểm tra xem tên đăng nhập đã tồn tại chưa
            // var isAvailable = await _userService.IsUsernameAvailable(request.Username);

            // Mô phỏng kiểm tra (trong ứng dụng thực tế bạn sẽ truy vấn database)
            var isAvailable = request.Username?.ToLower() != "existinguser";

            return Ok(isAvailable);
        }

        [HttpPost("checkEmailAvailability")]
        public async Task<IActionResult> CheckEmailAvailability([FromBody] CheckEmailRequest request)
        {
            if (string.IsNullOrEmpty(request?.Email))
            {
                return BadRequest("Email không được để trống.");
            }

            // Gọi service/repository để kiểm tra xem email đã được sử dụng chưa
            // var isAvailable = await _userService.IsEmailAvailable(request.Email);

            // Mô phỏng kiểm tra (trong ứng dụng thực tế bạn sẽ truy vấn database)
            var isAvailable = request.Email?.ToLower() != "used@example.com";

            return Ok(isAvailable);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (request.Password != request.ConfirmPassword)
            {
                return BadRequest("Mật khẩu không khớp.");
            }

            if (!request.Terms)
            {
                return BadRequest("Bạn cần đồng ý với Điều khoản và Điều kiện.");
            }

            // Gọi service/repository để đăng ký người dùng
            // var registrationResult = await _userService.RegisterUser(request);

            // Mô phỏng đăng ký thành công
            // if (registrationResult.Success)
            // {
            //     return Ok(new RegistrationResponse { Message = "Đăng ký thành công", UserId = registrationResult.UserId });
            // }
            // else
            // {
            //     return BadRequest(new ErrorResponse { Message = registrationResult.ErrorMessage });
            // }

            // Mô phỏng đăng ký thành công (hardcoded)
            return Ok(new RegistrationResponse { Message = "Đăng ký thành công", UserId = Guid.NewGuid().ToString() });
        }
    }

    public class CheckUsernameRequest
    {
        public string Username { get; set; }
    }

    public class CheckEmailRequest
    {
        public string Email { get; set; }
    }

    public class RegistrationRequest
    {
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Gender { get; set; }
        public bool Terms { get; set; }
    }

    public class RegistrationResponse
    {
        public string Message { get; set; }
        public string UserId { get; set; }
        // ... các thông tin khác từ backend
    }

    public class ErrorResponse
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }
        // ... các thông tin lỗi khác từ backend
    }
// }