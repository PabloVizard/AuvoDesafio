﻿using CsvHelper;
using CsvHelper.Configuration;
using Domain.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly string _filePath;

        public BaseRepository(string filePath)
        {
            _filePath = filePath;
        }

        public async Task<List<T>> GetAllAsync()
        {
            var file = _filePath;

            var database = new List<T>();

            var databaseCSV = await ReadFileAsync(file);
            lock (database)
            {
                database.AddRange(databaseCSV);
            }

            return database;
        }

        protected virtual async Task<List<T>> ReadFileAsync(string filePath)
        {
            var databaseCSV = new List<T>();

            using var reader = new StreamReader(filePath, Encoding.GetEncoding("ISO-8859-1"));
            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";"
            };
            using var csv = new CsvReader(reader, csvConfig);
            csv.Read();
            csv.ReadHeader();
            var records = await csv.GetRecordsAsync<T>().ToListAsync();
            databaseCSV.AddRange(records);

            return databaseCSV;
        }
    }
}
