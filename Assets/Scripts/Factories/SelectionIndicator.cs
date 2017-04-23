using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Factories
{
    public static class SelectionIndicatorFactory
    {
        public static MinMaxRect GetFarthestCorners(Transform selectedObject, MouseController mouseController)
        {
            return GetFartherstScreenCorners(GetObjectCorners(selectedObject, mouseController));
        }
        


        static Vector3[] GetObjectCorners(Transform selectedObject, MouseController mouseController)
        {
            Bounds objectBounds = selectedObject.GetComponent<BoxCollider>().bounds;
            Vector3[] screenSpaceCorners = new Vector3[8];

            screenSpaceCorners[0] = mouseController.mainCam.WorldToScreenPoint(new Vector3(objectBounds.center.x + objectBounds.extents.x, objectBounds.center.y + objectBounds.extents.y, objectBounds.center.z + objectBounds.extents.z));
            screenSpaceCorners[1] = mouseController.mainCam.WorldToScreenPoint(new Vector3(objectBounds.center.x + objectBounds.extents.x, objectBounds.center.y + objectBounds.extents.y, objectBounds.center.z - objectBounds.extents.z));
            screenSpaceCorners[2] = mouseController.mainCam.WorldToScreenPoint(new Vector3(objectBounds.center.x + objectBounds.extents.x, objectBounds.center.y - objectBounds.extents.y, objectBounds.center.z + objectBounds.extents.z));
            screenSpaceCorners[3] = mouseController.mainCam.WorldToScreenPoint(new Vector3(objectBounds.center.x + objectBounds.extents.x, objectBounds.center.y - objectBounds.extents.y, objectBounds.center.z - objectBounds.extents.z));

            screenSpaceCorners[4] = mouseController.mainCam.WorldToScreenPoint(new Vector3(objectBounds.center.x - objectBounds.extents.x, objectBounds.center.y + objectBounds.extents.y, objectBounds.center.z + objectBounds.extents.z));
            screenSpaceCorners[5] = mouseController.mainCam.WorldToScreenPoint(new Vector3(objectBounds.center.x - objectBounds.extents.x, objectBounds.center.y + objectBounds.extents.y, objectBounds.center.z - objectBounds.extents.z));
            screenSpaceCorners[6] = mouseController.mainCam.WorldToScreenPoint(new Vector3(objectBounds.center.x - objectBounds.extents.x, objectBounds.center.y - objectBounds.extents.y, objectBounds.center.z + objectBounds.extents.z));
            screenSpaceCorners[7] = mouseController.mainCam.WorldToScreenPoint(new Vector3(objectBounds.center.x - objectBounds.extents.x, objectBounds.center.y - objectBounds.extents.y, objectBounds.center.z - objectBounds.extents.z));

            return screenSpaceCorners;
        }

        static MinMaxRect GetFartherstScreenCorners(Vector3[] corners)
        {
            MinMaxRect fartherstCorners = new MinMaxRect();

            fartherstCorners.minX = corners[0].x;
            fartherstCorners.minY = corners[0].y;
            fartherstCorners.maxX = corners[0].x;
            fartherstCorners.maxY = corners[0].y;

            for (int i = 1; i < 8; i++)
            {
                if (corners[i].x < fartherstCorners.minX)
                {
                    fartherstCorners.minX = corners[i].x;
                }
                if (corners[i].y < fartherstCorners.minY)
                {
                    fartherstCorners.minY = corners[i].y;
                }
                if (corners[i].x > fartherstCorners.maxX)
                {
                    fartherstCorners.maxX = corners[i].x;
                }
                if (corners[i].y > fartherstCorners.maxY)
                {
                    fartherstCorners.maxY = corners[i].y;
                }

            }
            return fartherstCorners;
        }

        

    }

    public struct MinMaxRect
    {
        public float minX, minY, maxX, maxY;
    }
}