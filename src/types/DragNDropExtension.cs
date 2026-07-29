using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using LetheAISharp;

namespace LetheChat
{
    public static class DragNDropExtension
    {
        public static List<string> DroppedFilePath { get; set; } = [];

        /// <summary>
        /// Enables drag and drop functionality for images on a control
        /// </summary>
        /// <param name="control">The control to enable drag and drop for</param>
        /// <param name="onImageDropped">Action to execute when an image is dropped, receives the base64 string</param>
        /// <param name="maxSize">Maximum size to scale images to (optional)</param>
        public static void EnableImageDragDrop(this Control control, Action<string> onImageDropped, int maxSize = 0)
        {
            // Enable drag & drop
            control.AllowDrop = true;

            // Handle the drag-enter event to provide visual feedback
            control.DragEnter += (sender, e) =>
            {
                // Check if the dragged data contains file(s)
                if (e.Data?.GetDataPresent(DataFormats.FileDrop) == true)
                {
                    string[]? files = e.Data.GetData(DataFormats.FileDrop) as string[];

                    // Check if at least one file is an image
                    if (files?.Length > 0 && IsImageFile(files[0]))
                    {
                        e.Effect = DragDropEffects.Copy; // Show the copy cursor
                        return;
                    }
                }

                e.Effect = DragDropEffects.None; // Show the "no drop" cursor
            };

            // Handle the actual drop event
            control.DragDrop += (sender, e) =>
            {
                // Get the dropped files
                string[]? files = e.Data?.GetData(DataFormats.FileDrop) as string[];

                if (files?.Length > 0)
                {
                    foreach (var filePath in files)
                    {
                        // Check if it's an image file
                        if (IsImageFile(filePath))
                        {
                            try
                            {
                                // Convert the image to base64
                                string? base64Image = ImageUtils.ImageToBase64(filePath, maxSize);

                                if (base64Image != null)
                                {
                                    // Call the provided action with the base64 string
                                    DroppedFilePath.Add(filePath);
                                    onImageDropped(base64Image);
                                }
                                else
                                {
                                    MessageBox.Show("Failed to convert image to base64.", "Error",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Error processing image: {ex.Message}", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please drop a valid image file.", "Invalid File",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            };
        }

        /// <summary>
        /// Checks if a file is an image based on its extension
        /// </summary>
        private static bool IsImageFile(string filePath)
        {
            string extension = Path.GetExtension(filePath).ToLowerInvariant();
            return extension == ".jpg" || extension == ".jpeg" ||
                   extension == ".png" || extension == ".gif" ||
                   extension == ".bmp" || extension == ".tiff" ||
                   extension == ".webp";
        }
    }
}