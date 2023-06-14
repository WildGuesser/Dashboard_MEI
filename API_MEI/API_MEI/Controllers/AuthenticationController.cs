using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using API_MEI.Data;
using API_MEI.Models;
using AutoMapper;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using API_MEI.DTOs;

namespace API_MEI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly API_MEIContext _context;
        private readonly IMapper _mapper;

        public AuthenticationController(API_MEIContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public IActionResult Login(User user)
        {
            // Perform authentication logic
            if (IsValidUser(user))
            {
                // Authentication successful
                return Ok("Authentication successful");
            }
            else
            {
                // Authentication failed
                return Unauthorized("Invalid username or password");
            }
        }

        private bool IsValidUser(User user)
        {
            // Validate against hardcoded admin username and password
            string adminUsername = "admin";
            string adminPassword = "password";

            return user.Username == adminUsername && user.Password == adminPassword;
        }
    }
}
