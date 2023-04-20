// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLabelCore.WinForms.Helpers;

public sealed class WsFontsSettingsHelper
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618
    private static WsFontsSettingsHelper _instance;
#pragma warning restore CS8618
    public static WsFontsSettingsHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields and properties

    public Font FontMinimum { get; private set; }
    public Font FontLabelsGray { get; private set; }
    public Font FontButtons { get; private set; }
    public Font FontButtonsSmall { get; private set; }
    public Font FontLabelsBlack { get; private set; }
    public Font FontLabelsTitle { get; private set; }
    public Font FontLabelsMaximum { get; private set; }

    #endregion

    #region Constructor and destructor

    public WsFontsSettingsHelper()
    {
        Resize(7.00f);
    }

    public void Transform(int width, int height)
    {
        float baseSize = width switch
        {
            >= 1920 when height >= 1080 => 15.00f,
            >= 1600 when height >= 1024 => 13.00f,
            >= 1366 when height >= 768 => 11.00f,
            >= 1024 when height >= 768 => 9.00f,
            _ => 8.00f
        };
        Resize(baseSize);
    }

    private void Resize(float baseSize)
    {
        FontMinimum = new("Microsoft Sans Serif", baseSize, FontStyle.Regular, GraphicsUnit.Point, 204);
        FontLabelsGray = new("Microsoft Sans Serif", baseSize + 1.00f, FontStyle.Regular, GraphicsUnit.Point, 204);
        FontButtons = new("Microsoft Sans Serif", baseSize + 4.00f, FontStyle.Regular, GraphicsUnit.Point, 204);
        FontButtonsSmall = new("Microsoft Sans Serif", baseSize + 2.00f, FontStyle.Regular, GraphicsUnit.Point, 204);
        FontLabelsBlack = new("Microsoft Sans Serif", baseSize + 4.00f, FontStyle.Regular, GraphicsUnit.Point, 204);
        FontLabelsTitle = new("Microsoft Sans Serif", baseSize + 5.00f, FontStyle.Regular, GraphicsUnit.Point, 204);
        FontLabelsMaximum = new("Microsoft Sans Serif", baseSize + 16.00f, FontStyle.Bold, GraphicsUnit.Point, 204);
    }

    public void Close()
    {
        FontMinimum.Dispose();
        FontLabelsGray.Dispose();
        FontButtons.Dispose();
        FontButtonsSmall.Dispose();
        FontLabelsBlack.Dispose();
        FontLabelsTitle.Dispose();
        FontLabelsMaximum.Dispose();
    }

    #endregion
}