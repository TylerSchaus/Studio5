using UnityEngine;

namespace _Scripts.Utils
{
    public class HotSwapColor : MonoBehaviour
    {
        [SerializeField] private Color color;
        [SerializeField] private MeshRenderer mr;

        private MaterialPropertyBlock mpb;
        private static readonly int ShaderProp = Shader.PropertyToID("_BaseColor");

        private MaterialPropertyBlock Mpb => mpb ??= new MaterialPropertyBlock();

        private void OnEnable()
        {
            mr = GetComponent<MeshRenderer>();
            if(mr == null) Debug.LogWarning("No mesh renderer found!");
            ApplyColor();
        }

        private void OnValidate()
        {
            mr = GetComponent<MeshRenderer>();
            if(mr == null) Debug.LogWarning("No mesh renderer found!");
            ApplyColor();
        }

        public void SetColor(Color color)
        {
            this.color = color;
            ApplyColor();
        }

        private void ApplyColor()
        {
            Mpb.SetColor(ShaderProp, color);
            if(mr != null){
                mr.SetPropertyBlock(Mpb);
            }
        }
    }
}