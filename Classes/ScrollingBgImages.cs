using Godot;
using System;

[GlobalClass]
public partial class ScrollingBgImages : Resource
{
    [Export] public Godot.Collections.Array<Texture2D> Images { get; set; } = new Godot.Collections.Array<Texture2D>();

    public ScrollingBgImages() { }
    
}
