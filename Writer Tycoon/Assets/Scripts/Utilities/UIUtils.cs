using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace GhostWriter.Utilities.UI
{
    public static class UIUtils
    {
        public struct ImageData
        {
            public Image Image;
            public Color Color;

            public ImageData(Image image)
            {
                Image = image;
                Color = image.color;
            }
        }

        public struct TextData
        {
            public Text Text;
            public Color Color;

            public TextData(Text text)
            {
                Text = text;
                Color = text.color;
            }
        }

        /// <summary>
        /// Fade in Images and Texts
        /// </summary>
        public static async Task FadeIn(List<ImageData> images, List<TextData> texts, float fadeDuration, bool activateAll)
        {
            List<Task> imageTasks = new();
            List<Task> textTasks = new();

            // Add each image fade-in as a task
            foreach (ImageData imageData in images)
            {
                imageTasks.Add(FadeInImage(imageData.Image, imageData.Color, fadeDuration, activateAll));
            }

            // Add each text fade-in as a task
            foreach (TextData textData in texts)
            {
                textTasks.Add(FadeInText(textData.Text, textData.Color, fadeDuration, activateAll));
            }

            // Await until all tasks are finished
            await Task.WhenAll(imageTasks);
            await Task.WhenAll(textTasks);
        }

        /// <summary>
        /// Fade out Images and Texts
        /// </summary>
        public static async Task FadeOut(List<ImageData> images, List<TextData> texts, float fadeDuration, bool deactivateAll)
        {
            List<Task> imageTasks = new List<Task>();
            List<Task> textTasks = new List<Task>();

            // Add each image fade-out as a task
            foreach (ImageData imageData in images)
            {
                imageTasks.Add(FadeOutImage(imageData.Image, imageData.Color, fadeDuration, deactivateAll));
            }

            // Add each text fade-out as a task
            foreach (TextData textData in texts)
            {
                textTasks.Add(FadeOutText(textData.Text, textData.Color, fadeDuration, deactivateAll));
            }

            // Await until all tasks are finished
            await Task.WhenAll(imageTasks);
            await Task.WhenAll(textTasks);
        }

        /// <summary>
        /// Fade in a Text object
        /// </summary>
        public static async Task FadeInText(Text text, Color textColor, float duration, bool activate)
        {
            if (!text.gameObject.activeSelf && activate) text.gameObject.SetActive(true);

            // Set the elapsed time
            float elapsedTime = 0f;

            // Get the current color
            Color color = text.color;

            // Go through the duration
            while (elapsedTime < duration)
            {
                // Increment elapsed time
                elapsedTime += Time.deltaTime;

                // Clamp the opacity to the time
                color.a = Mathf.Clamp(elapsedTime / duration, 0f, textColor.a);

                // Set the color
                text.color = color;

                // Yield to allow other tasks to happen
                await Task.Yield();
            }

            // Set the color to be fully visible
            color.a = textColor.a;
            text.color = color;
        }

        /// <summary>
        /// Fade out a Text object
        /// </summary>
        public static async Task FadeOutText(Text text, Color textColor, float duration, bool deactivate)
        {
            // Set the elapsed time
            float elapsedTime = 0f;

            // Get the current color
            Color color = text.color;

            // Go through the duration
            while (elapsedTime < duration)
            {
                // Increment elapsed time
                elapsedTime += Time.deltaTime;

                // Clamp the opacity to the time
                color.a = textColor.a - Mathf.Clamp(elapsedTime / duration, 0f, textColor.a);

                // Set the color
                text.color = color;

                // Yield to allow other tasks to happen
                await Task.Yield();
            }

            // Set the color to be fully visible
            color.a = 0f;
            text.color = color;

            // De-activate the game object
            if (deactivate) text.gameObject.SetActive(false);
        }

        /// <summary>
        /// Fade in an Image object
        /// </summary>
        public static async Task FadeInImage(Image image, Color imageColor, float duration, bool activate)
        {
            if (!image.gameObject.activeSelf && activate) image.gameObject.SetActive(true);

            // Set the elapsed time
            float elapsedTime = 0f;

            // Get the current color
            Color color = image.color;

            // Go through the duration
            while (elapsedTime < duration)
            {
                // Increment elapsed time
                elapsedTime += Time.deltaTime;

                // Clamp the opacity to the time
                color.a = Mathf.Clamp(elapsedTime / duration, 0f, imageColor.a);

                // Set the color
                image.color = color;

                // Yield to allow other tasks to happen
                await Task.Yield();
            }

            // Set the color to be fully visible
            color.a = imageColor.a;
            image.color = color;
        }

        /// <summary>
        /// Fade out an Image object
        /// </summary>
        public static async Task FadeOutImage(Image image, Color imageColor, float duration, bool deactivate)
        {
            // Set the elapsed time
            float elapsedTime = 0f;

            // Get the current color
            Color color = image.color;

            // Go through the duration
            while (elapsedTime < duration)
            {
                // Increment elapsed time
                elapsedTime += Time.deltaTime;

                // Clamp the opacity to the time
                color.a = imageColor.a - Mathf.Clamp(elapsedTime / duration, 0f, imageColor.a);

                // Set the color
                image.color = color;

                // Yield to allow other tasks to happen
                await Task.Yield();
            }

            // Set the color to be fully visible
            color.a = 0f;
            image.color = color;

            // De-activate the game object
            if (deactivate) image.gameObject.SetActive(false);
        }
    }
}