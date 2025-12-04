using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using MauiApp1.Models;
using Microsoft.Maui.Storage;
using System.IO;

namespace MauiApp1.Services
{
    public static class DatabaseService
    {
        static SQLiteAsyncConnection db;

        static async Task Init()
        {
            if (db != null)
                return;

            try
            {
                var dbPath = Path.Combine(FileSystem.AppDataDirectory, "matchmate.db");
                
                db = new SQLiteAsyncConnection(dbPath);
                await db.CreateTableAsync<UserProfile>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SQLite initialization failed: {ex}");
                throw;
            }
        }

        private static void NewMethod(string dbPath)
        {
            Console.WriteLine("DB path: " + dbPath);
        }

        public static async Task AddUserAsync(UserProfile profile)
        {
            await Init();
            await db.InsertAsync(profile);
        }

        public static async Task<List<UserProfile>> GetUsersAsync()
        {
            await Init();
            return await db.Table<UserProfile>().ToListAsync();
        }

        public static async Task UpdateUserAsync(UserProfile profile)
        {
            await Init();
            await db.UpdateAsync(profile);
        }

        public static async Task DeleteUserAsync(UserProfile profile)
        {
            await Init();
           
            await db.DeleteAsync(profile);
        }

       
        public static async Task DeleteAllUsersAsync()
        {
            await Init();
            await db.DeleteAllAsync<UserProfile>();
        }
    }
}
