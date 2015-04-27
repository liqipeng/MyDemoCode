using System;
using System.IO;

namespace _01WhatIsLucene
{
    class Config
    {
        public static readonly string IndexFolder = Path.Combine(new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName, "IndexFiles");

        public static readonly string TextFilesFolder = Path.Combine(new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName, "TextFiles");
        public static readonly string FileSearchPattern = "*.cs";
        public static readonly string FileExt = ".cs";

        public const string Field_Path = "Path";
        public const string Field_Name = "Name";
        public const string Field_Content = "Content";
    }
}
