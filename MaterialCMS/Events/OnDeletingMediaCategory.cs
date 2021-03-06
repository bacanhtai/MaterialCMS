﻿using System.Collections.Generic;
using System.Linq;
using MaterialCMS.Entities.Documents.Media;
using MaterialCMS.Services;

namespace MaterialCMS.Events
{
    public class OnDeletingMediaCategory : IOnDeleting<MediaCategory>
    {
        private readonly IFileService _fileService;

        public OnDeletingMediaCategory(IFileService fileService)
        {
            _fileService = fileService;
        }

        public void Execute(OnDeletingArgs<MediaCategory> args)
        {
            MediaCategory category = args.Item;
            if (category == null)
                return;
            List<MediaFile> mediaFiles = category.Files.ToList();

            foreach (MediaFile mediaFile in mediaFiles)
            {
                _fileService.DeleteFile(mediaFile);
            }
            _fileService.RemoveFolder(category);
        }
    }
}