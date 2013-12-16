using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Bouncing_Bravery
{
    class Camera
    {
        public Vector3 thirdPersonReference = new Vector3(0, GameConstants.thirdPersonRef *0.5f, -GameConstants.thirdPersonRef);
        public Vector3 FixedReference = new Vector3(0, GameConstants.thirdPersonRef * 1.7f, -GameConstants.thirdPersonRef * 1.7f);
        public Matrix rotationMatrix = Matrix.CreateRotationX(MathHelper.PiOver2);
        public Matrix ProjectionMatrix;
        public Matrix ViewMatrix;
        private int currentTypeOfCamera;

        float aspectRatio;
        public Camera(float a)
        {
            currentTypeOfCamera = 0; //fixed
            aspectRatio = a;
        }

        public int ToggleCamera()
        {
            if (currentTypeOfCamera == 0) //if camera was fixed
            {
                currentTypeOfCamera = 1; //make it third person
            }
            else //camera was third person
            {
                currentTypeOfCamera = 0; //make it fixed
                rotationMatrix = Matrix.CreateRotationX(MathHelper.PiOver2); //reset the camera rotation
            }
            return currentTypeOfCamera;
        }

        public void Update(Vector3 avatarPosition, float avatarYaw)
        {
            if (currentTypeOfCamera == 0) //fixed camera
            {
                // Create a vector pointing the direction the camera is facing.
                Vector3 transformedReference = Vector3.Transform(FixedReference, rotationMatrix);

                // Calculate the position the camera is looking from.
                Vector3 cameraPosition = transformedReference + avatarPosition;
                ViewMatrix = Matrix.CreateLookAt(cameraPosition, avatarPosition, new Vector3(0.0f, 1.0f, 0.0f));

                ProjectionMatrix = Matrix.CreatePerspectiveFieldOfView(GameConstants.viewAngle, aspectRatio, GameConstants.nearClip, GameConstants.farClip);
            }
            else //third person
            {
                Matrix rotationMatrix = Matrix.CreateRotationY(avatarYaw);

                // Create a vector pointing the direction the camera is facing.
                Vector3 transformedReference = Vector3.Transform(thirdPersonReference, rotationMatrix);

                // Calculate the position the camera is looking from.
                Vector3 cameraPosition = transformedReference + avatarPosition;
                ViewMatrix = Matrix.CreateLookAt(cameraPosition, avatarPosition, new Vector3(0.0f, 1.0f, 0.0f));

                ProjectionMatrix = Matrix.CreatePerspectiveFieldOfView(GameConstants.viewAngle, aspectRatio, GameConstants.nearClip, GameConstants.farClip);
            }
        }
    }
}
