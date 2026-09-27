using UnityEngine;
using System.Collections.Generic; 
using Nova;


public class NovaButtonManager : MonoBehaviour
{
    [SerializeField]
    private List<UIBlock2D> novaButtons;

    [SerializeField]
    public AudioClip hoverSound;

    void Start()
    {
        // Subscribe to the hover event for each button
        foreach (var button in novaButtons)
        {
            button.AddGestureHandler<Gesture.OnHover>(HandleHover);
        }
    }

    private void HandleHover(Gesture.OnHover hoverEvent)
    {
        // Play the hover sound for the button
        AudioManagerMainMenu.Instance.PlaySound(hoverSound);
    }
}
