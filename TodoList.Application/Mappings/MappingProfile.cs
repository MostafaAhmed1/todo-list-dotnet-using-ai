using AutoMapper;
using TodoList.Domain.Entities;
using TodoList.Application.DTOs;

namespace TodoList.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TodoItem, TodoItemDto>();
            CreateMap<CreateTodoItemDto, TodoItem>();
            CreateMap<UpdateTodoItemDto, TodoItem>();
        }
    }
}