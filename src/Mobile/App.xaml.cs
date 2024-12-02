﻿namespace Mobile;

/// <inheritdoc />
public partial class App : Application
{
    /// <inheritdoc />
    public App()
    {
        InitializeComponent();
    }

    /// <inheritdoc />
    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new AppShell());
    }
}
