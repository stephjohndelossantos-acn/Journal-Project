// Services/IItemService.cs
using Journal.Data;
using Journal.Models;
using JournalAPI.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using JournalAPI.Services;


namespace JournalAPI.Services
{
    public interface IItemService
    {
        Task<List<ItemDto>> GetByTypeAsync(int? typeId);
        Task<ItemDto?> GetByIdAsync(int id);
        Task<ItemDto?> CreateAsync(CreateItemDto dto);   // null if the type doesn't exist
        Task<bool> UpdateAsync(int id, CreateItemDto dto);
        Task<bool> DeleteAsync(int id);
    }
}