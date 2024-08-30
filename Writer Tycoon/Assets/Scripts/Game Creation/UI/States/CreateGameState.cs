using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using WriterTycoon.Patterns.StateMachine;
using static WriterTycoon.Utilities.UI.UIUtils;

namespace WriterTycoon.GameCreation.UI.States
{
    public class CreateGameState : IState
    {
        protected float fadeDuration;
        protected GameObject uiObject;
        protected List<ImageData> imageDatas = new();
        protected List<TextData> textDatas = new();

        public CreateGameState(GameObject uiObject)
        {
            fadeDuration = 0f;
            this.uiObject = uiObject;

            InstantiateUILists();
        }

        public virtual void OnEnter()
        {
        }

        public virtual void Update()
        {
        }

        public virtual void FixedUpdate()
        {
        }

        public virtual void OnExit()
        {
        }

        /// <summary>
        /// Show the UI object
        /// </summary>
        protected virtual async Task Show() => await FadeIn(imageDatas, textDatas, fadeDuration, true);

        /// <summary>
        /// Hide the UI object
        /// </summary>
        protected virtual async Task Hide() => await FadeOut(imageDatas, textDatas, fadeDuration, true);

        /// <summary>
        /// Instantiate the UI data lists
        /// </summary>
        private void InstantiateUILists()
        {
            // Sotre images and texts into lists
            List<Image> images = uiObject.GetComponentsInChildren<Image>(true).ToList();
            List<Text> texts = uiObject.GetComponentsInChildren<Text>(true).ToList();

            // Add Image Datas
            foreach(Image image in images)
            {
                imageDatas.Add(new ImageData(image));
            }

            foreach(Text text in texts)
            {
                textDatas.Add(new TextData(text));
            }
        }

        /// <summary>
        /// Make all Images and Texts invisible
        /// </summary>
        protected virtual void MakeElementsInvisible()
        {
            // Loop through each Image Data
            foreach (ImageData imageData in imageDatas)
            {
                // Make the color invisible
                Color invisible = imageData.Color;
                invisible.a = 0f;
                imageData.Image.color = invisible;
            }

            // Loop through each Text Data
            foreach (TextData textData in textDatas)
            {
                // Make the color invisible
                Color invisible = textData.Color;
                invisible.a = 0f;
                textData.Text.color = invisible;
            }
        }
    }
}