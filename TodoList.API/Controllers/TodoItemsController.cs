using Microsoft.AspNetCore.Mvc;
using TodoList.Domain.Entities;
using TodoList.Domain.Interfaces;
using AutoMapper;
using TodoList.Application.DTOs;

namespace TodoList.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoItemsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TodoItemsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDto>>> GetAll()
        {
            var items = await _unitOfWork.TodoItems.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<TodoItemDto>>(items));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDto>> GetById(int id)
        {
            var item = await _unitOfWork.TodoItems.GetByIdAsync(id);
            if (item == null)
                return NotFound();

            return Ok(_mapper.Map<TodoItemDto>(item));
        }

        [HttpPost]
        public async Task<ActionResult<TodoItemDto>> Create(CreateTodoItemDto createDto)
        {
            var item = _mapper.Map<TodoItem>(createDto);
            item.CreatedDate = DateTime.UtcNow;
            
            await _unitOfWork.TodoItems.AddAsync(item);
            await _unitOfWork.SaveChangesAsync();

            var itemDto = _mapper.Map<TodoItemDto>(item);
            return CreatedAtAction(nameof(GetById), new { id = item.Id }, itemDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateTodoItemDto updateDto)
        {
            var existingItem = await _unitOfWork.TodoItems.GetByIdAsync(id);
            if (existingItem == null)
                return NotFound();

            _mapper.Map(updateDto, existingItem);
            
            if (updateDto.IsCompleted && !existingItem.IsCompleted)
                existingItem.CompletedDate = DateTime.UtcNow;
            else if (!updateDto.IsCompleted)
                existingItem.CompletedDate = null;

            await _unitOfWork.TodoItems.UpdateAsync(existingItem);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _unitOfWork.TodoItems.GetByIdAsync(id);
            if (item == null)
                return NotFound();

            await _unitOfWork.TodoItems.DeleteAsync(item);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }
    }
}