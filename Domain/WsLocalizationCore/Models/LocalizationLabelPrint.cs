namespace WsLocalizationCore.Models;

[DebuggerDisplay("{ToString()}")]
public sealed class LocalizationLabelPrint : LocalizationBase
{
    #region Public and private fields, properties, constructor

    public LocalizationLabelPrint()
    {
        LocalizationLoader.Instance.FileLanguageLoaders.Add(new JsonFileLoader());
        string fileName = Path.Combine(Directory.GetCurrentDirectory(), @"Locales\LabelPrint.loc.json");
        if (File.Exists(fileName))
            LocalizationLoader.Instance.AddFile(fileName);
    }

    #endregion

    #region Public and private methods
    
    public string AlreadyRunning => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(AlreadyRunning)}");
    public string App => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(App)}");
    public string AppLoad => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(AppLoad)}");
    public string ButtonPlu => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(ButtonPlu)}");
    public string ButtonSetKneading => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(ButtonSetKneading)}");
    public string CheckAllPassed => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(CheckAllPassed)}");
    public string CheckWeightIsZero => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(CheckWeightIsZero)}");
    public string ContactWithAdmin => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(ContactWithAdmin)}");
    public string Count => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(Count)}");
    public string Error => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(Error)}");
    public string FieldDate => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(FieldDate)}");
    public string FieldKneading => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(FieldKneading)}");
    public string FieldPalletSize => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(FieldPalletSize)}");
    public string FieldProductDate => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(FieldProductDate)}");
    public string FieldWeightNetto => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(FieldWeightNetto)}");
    public string FieldWeightTare => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(FieldWeightTare)}");
    public string Host => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(Host)}");
    public string IsDataNotExists => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(IsDataNotExists)}");
    public string LabelContextExpirationDt => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(LabelContextExpirationDt)}");
    public string LabelContextKneading => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(LabelContextKneading)}");
    public string LabelContextNesting => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(LabelContextNesting)}");
    public string LabelContextPlu => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(LabelContextPlu)}");
    public string LabelContextProductDt => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(LabelContextProductDt)}");
    public string LabelContextWeight => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(LabelContextWeight)}");
    public string LabelContextWorkShop => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(LabelContextWorkShop)}");
    public string Labels => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(Labels)}");
    public string Line => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(Line)}");
    public string LineForHost => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(LineForHost)}");
    public string LowerValue => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(UpperValue)}");
    public string MassaIsNotCalc => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(MassaIsNotCalc)}");
    public string MassaK => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(MassaK)}");
    public string MassaWaitStable => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(MassaWaitStable)}");
    public string Method => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(Method)}");
    public string NettoWeight => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(NettoWeight)}");
    public string NewGenderWoman => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(NewGenderWoman)}");
    public string NominalValue => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(NominalValue)}");
    public string NotFoundGenderMan => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(NotFoundGenderMan)}");
    public string NotFoundGenderWoman => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(NotFoundGenderWoman)}");
    public string OperationControl => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(OperationControl)}");
    public string Pallet => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(Pallet)}");
    public string Plu => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(Plu)}");
    public string PluCount => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(PluCount)}");
    public string PluEan13IsNotSet => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(PluEan13IsNotSet)}");
    public string PluginDefault => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(PluginDefault)}");
    public string PluginLabel => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(PluginLabel)}");
    public string PluginMassa => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(PluginMassa)}");
    public string PluginMemory => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(PluginMemory)}");
    public string PluginPrintTsc => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(PluginPrintTsc)}");
    public string PluginPrintZebra => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(PluginPrintZebra)}");
    public string PluGtinIsNotSet => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(PluGtinIsNotSet)}");
    public string PluIsPiece => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(PluIsPiece)}");
    public string PluIsWeight => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(PluIsWeight)}");
    public string PluNotSelect => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(PluNotSelect)}");
    public string PluPackageNotSelect => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(PluPackageNotSelect)}");
    public string PluPage => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(PluPage)}");
    public string PluTemplateIsNotSet => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(PluTemplateIsNotSet)}");
    public string PluWeight => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(PluWeight)}");
    public string ProductWeight => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(ProductWeight)}");
    public string ProgramIsNotFound => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(ProgramIsNotFound)}");
    public string QuestionCloseApp => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(QuestionCloseApp)}");
    public string QuestionRunApp => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(QuestionRunApp)}");
    public string QuestionWriteToDb => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(QuestionWriteToDb)}");
    public string ScreenResolution => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(ScreenResolution)}");
    public string SetArea => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(SetArea)}");
    public string SwitchKneading => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(SwitchKneading)}");
    public string SwitchLine => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(SwitchLine)}");
    public string SwitchPluLine => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(SwitchPluLine)}");
    public string SwitchPluNesting => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(SwitchPluNesting)}");
    public string Terminal => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(Terminal)}");
    public string UpperValue => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(UpperValue)}");
    public string WeightGenderMan => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(WeightGenderMan)}");
    public string WeightingControl => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(WeightingControl)}");
    public string WeightingIsCalc => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(WeightingIsCalc)}");
    public string WeightUnitKg => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(WeightUnitKg)}");

    #endregion

    #region Public and private methods

    public override string ToString() => $"{Lang}";
    public string ButtonNewPallet() => $"{NewGenderWoman}{Environment.NewLine}{Pallet}";
    public string ButtonRunScalesTerminal() => $"{WeightGenderMan}{Environment.NewLine}{Terminal}";
    public string CheckWeightThreshold(decimal weightNet) => $"{WeightingControl} {ProductWeight}: {weightNet:0.000} {WeightUnitKg}";
    public string CheckWeightThresholds(decimal currentNet, decimal upperWeightThreshold, decimal nominalWeight, decimal lowerWeightThreshold) => $"{WeightingControl} {NettoWeight}: {currentNet:0.000} {WeightUnitKg} {UpperValue}: {upperWeightThreshold:0.000} {WeightUnitKg} {NominalValue}: {nominalWeight:0.000} {WeightUnitKg} {LowerValue}: {lowerWeightThreshold:0.000} {WeightUnitKg}";
    public string HostNotFound(string deviceName) => $"{Host} '{deviceName}' {NotFoundGenderMan}";
    public string IsException(string? message) => $"{Error}! {message}";
    public string PluCountNesting(short nesting) => $"{Plu} ({nesting} {Count})";
    public string ProgramNotFound(string fileName) => $"{ProgramIsNotFound}!" + Environment.NewLine + fileName + Environment.NewLine + $"{ContactWithAdmin}.";
    public string RegistrationWarningLineNotFound(string deviceName) => $"{LineForHost} '{deviceName}' {NotFoundGenderWoman}!";
    public string SetAreaWithParam(long id, string name) => $"{SetArea}: {id} | {name}";
    
    #endregion
}