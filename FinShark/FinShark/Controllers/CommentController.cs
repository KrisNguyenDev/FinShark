﻿using AutoMapper;
using FinShark.Data;
using FinShark.Dtos.Comment;
using FinShark.Interfaces;
using FinShark.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FinShark.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly ICommentRepository _commentRepository;
        private readonly IStockRepository _stockRepository;
        private readonly IMapper _mapper;

        public CommentController(ApplicationDBContext context, ICommentRepository commentRepository, IStockRepository stockRepository, IMapper mapper)
        {
            _context = context;
            _commentRepository = commentRepository;
            _stockRepository = stockRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _commentRepository.GetAllAsync();

            return Ok(_mapper.Map<List<CommentDto>>(comments));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var comment = await _commentRepository.GetByIdAsync(id);

            if (comment == null)
            {
                return BadRequest("Comment not found.");
            }

            return Ok(_mapper.Map<CommentDto>(comment));
        }

        [HttpPost("{stockId:int}")]
        public async Task<IActionResult> Create([FromRoute] int stockId, [FromBody] CreateCommentRequest request)
        {
            Stock stock = await _stockRepository.GetByIdAsync(stockId);

            if (stock is null)
            {
                return BadRequest("Stock does not exists.");
            }

            Comment comment = new Comment
            {
                Content = request.Content,
                Title = request.Title,
                StockId = request.StockId,
            };

            await _commentRepository.CreateAsync(comment);

            return CreatedAtAction(nameof(GetById), new { id = comment.Id }, comment);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var result = await _commentRepository.DeleteAsync(id);

            if (result == 0)
            {
                return BadRequest("Comment not found.");
            }

            return NoContent();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentRequest request)
        {
            if (id != request.Id)
            {
                return BadRequest("Comment not found.");
            }

            Comment comment = new Comment
            {
                Id = request.Id,
                Title = request.Title,
                Content = request.Content,
            };

            await _commentRepository.UpdateAsync(id, comment);

            return NoContent();
        }
    }
}
