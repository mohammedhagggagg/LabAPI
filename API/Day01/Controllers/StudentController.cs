using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Day01.Data;
using Day01.DTO;
using Day01.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Day01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(AppNameHeaderFilter))]
    public class StudentController : ControllerBase
    {
        private readonly UserManager<Student> usermanager;
        private readonly RoleManager<IdentityRole> rolemanager;
        private readonly IConfiguration config;
        private readonly AppDbContext _context;
        private new List<string> _allowedExtenstions = new List<string> { ".jpg", ".png" };

        public StudentController(UserManager<Student> usermanager, IConfiguration config, RoleManager<IdentityRole> rolemanager, AppDbContext context)
        {
            this.usermanager = usermanager;
            this.rolemanager = rolemanager;
            this.config = config;
            _context = context;
        }


        [HttpGet]
        [Authorize(Roles = "Student")]
        public async Task<ActionResult> GetAll()
        {
            var students = await _context.Students.ToListAsync();
            return Ok(students);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(string id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
                return NotFound();

            return Ok(student);
        }

        [HttpGet("GetByName/{name}")]
        public async Task<ActionResult> GetByName(string name)
        {
            var students = await _context.Students
                .Where(s => s.UserName == name)
                .ToListAsync();

            return students.Any() ? Ok(students) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromForm] StudentDTO studentDto)
        {
            if (studentDto == null) return BadRequest();

            if (studentDto.Image != null)
            {

                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

                var uniqueFileName = Guid.NewGuid().ToString() + "_" + studentDto.Image.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await studentDto.Image.CopyToAsync(fileStream);
                }

                studentDto.Photo = uniqueFileName;
            }
            else
            {
                return BadRequest("image is required.");
            }

            var student = new Student
            {
                UserName = studentDto.Name,
                Address = studentDto.Address,
                Image = studentDto.Photo,
                DepartmentId = studentDto.DeptId
            };

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = student.Id }, student);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(string id, [FromForm] StudentDTO studentDto)
        {
            if (studentDto == null) return BadRequest();

            var student = await _context.Students.FindAsync(id);
            if (student == null) return NotFound();

            if (studentDto.Image != null)
            {

                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

                var uniqueFileName = Guid.NewGuid().ToString() + "_" + studentDto.Image.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await studentDto.Image.CopyToAsync(fileStream);
                }

                studentDto.Photo = uniqueFileName;
                student.Image = studentDto.Photo;
            }
            else
            {
                student.Image = student.Image;
            }

            student.UserName = studentDto.Name;
            student.Address = studentDto.Address;
            student.DepartmentId = studentDto.DeptId;

            _context.Students.Update(student);
            await _context.SaveChangesAsync();

            return Ok(student);
        }

        [HttpPatch("EditName/{id}")]
        public async Task<IActionResult> EditName(string id, [FromQuery] string name)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
                return NotFound();

            student.UserName = name;
            _context.Students.Update(student);
            await _context.SaveChangesAsync();

            return Ok(student);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
                return NotFound();

            if (!string.IsNullOrEmpty(student.Image))
            {
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return Ok("Deleted");
        }


        [HttpPost("register")]
        public async Task<IActionResult> Registration([FromForm] StudentDTO studentDto)
        {
            if (studentDto == null) return BadRequest();

            if (studentDto.Image != null)
            {

                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

                var uniqueFileName = Guid.NewGuid().ToString() + "_" + studentDto.Image.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await studentDto.Image.CopyToAsync(fileStream);
                }

                studentDto.Photo = uniqueFileName;
            }
            else
            {
                return BadRequest("image is required.");
            }

            var student = new Student
            {
                Address = studentDto.Address,
                UserName = studentDto.userName,
                Email = studentDto.Email,
                Image = studentDto.Photo,
                DepartmentId = studentDto.DeptId
            };


            IdentityResult result = await usermanager.CreateAsync(student, studentDto.Password);
            if (result.Succeeded)
            {
                var role = "Student";
                await usermanager.AddToRoleAsync(student, role);
                await _context.SaveChangesAsync();

                var token = await usermanager.GenerateEmailConfirmationTokenAsync(student);
                var response = new
                {
                    Message = "Student added successfully",
                    UserName = student.UserName,
                    Email = student.Email,
                    ImageUrl = $"images/{student.Image}",
                    Token = token
                };
                return Ok(response);
            }
            return BadRequest(result.Errors.FirstOrDefault());
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDto userDto)
        {
            if (ModelState.IsValid == true)
            {
                //check - create token
                var user = await _context.Users.FirstOrDefaultAsync(c => c.Email == userDto.Email);
                if (user != null)//email found
                {
                    bool found = await usermanager.CheckPasswordAsync(user, userDto.Password);
                    if (found)
                    {
                        var claims = new List<Claim>();
                        claims.Add(new Claim(ClaimTypes.Email, user.Email));
                        claims.Add(new Claim(ClaimTypes.Name, user.UserName));
                        //claims.Add(new Claim(ClaimTypes.NameIdentifier, user.PasswordHash));
                        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

                        //get role
                        var roles = await usermanager.GetRolesAsync(user);
                        foreach (var itemRole in roles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, itemRole));
                        }
                        SecurityKey securityKey =
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Secret"]));

                        //Create token
                        JwtSecurityToken mytoken = new JwtSecurityToken(
                            issuer: config["JWT:ValidIssuer"],//url web api
                            audience: config["JWT:ValidAudiance"],//url consumer angular
                            expires: DateTime.Now.AddDays(double.Parse(config["JWT:DurationInDay"])),
                            claims: claims,
                            signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
                            );

                        if (user.Image == null)
                        {
                            var response = new
                            {
                                token = new JwtSecurityTokenHandler().WriteToken(mytoken),
                                expiration = mytoken.ValidTo,
                                email = user.Email,
                                userName = user.UserName,
                                image = "null",
                            };
                            return Ok(response);
                        }
                        else
                        {
                            var response2 = new
                            {
                                userId = user.Id,
                                token = new JwtSecurityTokenHandler().WriteToken(mytoken),
                                expiration = mytoken.ValidTo,
                                email = user.Email,
                                userName = user.UserName,
                                image = $"https://learningplatformv1.runasp.net/{user.Image}",
                            };
                            return Ok(response2);
                        }

                    }
                    return Ok("Email or password are invalid");
                }
                return Unauthorized();
            }
            return BadRequest();
        }


        [HttpPost("addAdminRole")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddAdmin(AddAdminRole userDto)
        {
            if (!ModelState.IsValid) return BadRequest();
            var user = await usermanager.FindByEmailAsync(userDto.Email);
            if (user == null) return BadRequest();
            if (!await rolemanager.RoleExistsAsync("Admin"))
            {
                return BadRequest("role already exists!");
            }
            await usermanager.AddToRoleAsync(user, "Admin");

            return Ok();
        }


        [HttpGet("HAndelExeption")]
        public async Task<IActionResult> ThrowExeption()
        {
            int Number = 0;
            int x = 5 / Number;

            return Ok();
        }
    }
}
