using AutoMapper;
using FinalProject.Data;
using FinalProject.DTOs;
using FinalProject.Entities;
using FinalProject.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FinalProject.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Mapper _mapper;

        public UsersController(IUnitOfWork unitOfWork, Mapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            var users = await _unitOfWork.UserRepository.GetMembersAsync();
            return Ok(users);
            
        }
        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            return await _unitOfWork.UserRepository.GetMemberAsync(username);
        }
        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _unitOfWork.UserRepository.GetUserByUsernameAsync(username);

            _mapper.Map(memberUpdateDto, user);
            _unitOfWork.UserRepository.Update(user);

            if(await _unitOfWork.Complete()) return NoContent();

            return BadRequest("Failed to update user");
        }

    }
}
