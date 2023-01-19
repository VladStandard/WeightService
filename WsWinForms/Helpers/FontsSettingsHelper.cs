// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsWinForms.Helpers;

public class FontsSettingsHelper
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618
    private static FontsSettingsHelper _instance;
#pragma warning restore CS8618
    public static FontsSettingsHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields and properties

    public Font FontButtons { get; private set; }
    public Font FontButtonsSmall { get; private set; }
    public Font FontMinimum { get; private set; }
    public Font FontLabelsBlack { get; private set; }
    public Font FontLabelsGray { get; private set; }
    public Font FontLabelsMaximum { get; private set; }
    public Font FontLabelsTitle { get; private set; }

    #endregion

    #region Constructor and destructor

    public FontsSettingsHelper()
    {
        Resize(7.00f);
    }

    public void Transform(int width, int height)
    {
        float baseSize;
        if (width >= 1920 && height >= 1080)
        {
            baseSize = 15.00f;
        }
        else if (width >= 1600 && height >= 1024)
        {
            baseSize = 13.00f;
        }
        else if (width >= 1366 && height >= 768)
        {
            baseSize = 11.00f;
        }
        else if (width >= 1024 && height >= 768)
        {
            baseSize = 9.00f;
        }
        else
        {
            baseSize = 8.00f;
        }

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

    #endregion
}