﻿#if NOVA_UI_EXISTS
using DA_Assets.Shared;
using DA_Assets.Shared.Extensions;
using Nova;
using System.Collections;
using UnityEngine;

namespace DA_Assets.FCU.Extensions
{
    internal static class NovaExtensions
    {
        public static IEnumerator SetNovaAnchor(this UIBlock uiBlock, AnchorType anchorType)
        {
            Vector3 worldPosition = uiBlock.transform.position;
            Alignment aligment = Alignment.TopLeft;

            switch (anchorType)
            {
                case AnchorType.TopLeft:
                    aligment = Alignment.TopLeft;
                    break;
                case AnchorType.TopCenter:
                    aligment = Alignment.TopCenter;
                    break;
                case AnchorType.TopRight:
                    aligment = Alignment.TopRight;
                    break;
                case AnchorType.MiddleLeft:
                    aligment = Alignment.CenterLeft;
                    break;
                case AnchorType.MiddleCenter:
                    aligment = Alignment.CenterCenter;
                    break;
                case AnchorType.MiddleRight:
                    aligment = Alignment.CenterRight;
                    break;
                case AnchorType.BottomLeft:
                    aligment = Alignment.BottomLeft;
                    break;
                case AnchorType.BottomCenter:
                    aligment = Alignment.BottomCenter;
                    break;
                case AnchorType.BottomRight:
                    aligment = Alignment.BottomRight;
                    break;
                case AnchorType.BottomStretch:
                    break;
                case AnchorType.VertStretchLeft:
                    break;
                case AnchorType.VertStretchRight:
                    break;
                case AnchorType.VertStretchCenter:
                    break;
                case AnchorType.HorStretchTop:
                    break;
                case AnchorType.HorStretchMiddle:
                    break;
                case AnchorType.HorStretchBottom:
                    break;
                case AnchorType.StretchAll:
                    break;
                default:
                    break;
            }

            uiBlock.Layout.Alignment = aligment;
            yield return WaitFor.Delay01();
            uiBlock.TrySetWorldPosition(worldPosition);
        }
    }
}
#endif