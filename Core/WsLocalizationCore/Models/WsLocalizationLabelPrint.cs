// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLocalizationCore.Models;

[DebuggerDisplay("{ToString()}")]
public sealed class WsLocalizationLabelPrint : WsLocalizationBase
{
    #region Public and private fields, properties, constructor

    public WsLocalizationLabelPrint()
    {
        LocalizationLoader.Instance.FileLanguageLoaders.Add(new JsonFileLoader());
        string fileName = Path.Combine(Directory.GetCurrentDirectory(), @"Locales\LabelPrint.loc.json");
        if (File.Exists(fileName))
            LocalizationLoader.Instance.AddFile(fileName);
    }

    #endregion

    #region Public and private methods

    private string SwitchMore => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(SwitchMore)}");
    public string AlreadyRunning => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(AlreadyRunning)}");
    public string AndArm => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(AndArm)}");
    public string AndLine => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(AndLine)}");
    public string App => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(App)}");
    public string AppExit => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(AppExit)}");
    public string AppExitDescription => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(AppExitDescription)}");
    public string AppLoad => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(AppLoad)}");
    public string AppLoadDescription => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(AppLoadDescription)}");
    public string AppWait => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(AppWait)}");
    public string AreFound => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(AreFound)}");
    public string AreNotFound => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(AreNotFound)}");
    public string Bundle => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(Bundle)}");
    public string ButtonAddKneading => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(ButtonAddKneading)}");
    public string ButtonPlu => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(ButtonPlu)}");
    public string ButtonScalesInit => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(ButtonScalesInit)}");
    public string ButtonScalesInitShort => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(ButtonScalesInitShort)}");
    public string ButtonSelectOrder => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(ButtonScalesInitShort)}");
    public string ButtonSetKneading => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(ButtonSetKneading)}");
    public string ButtonSettings => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(ButtonSettings)}");
    public string CheckAllPassed => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(CheckAllPassed)}");
    public string CheckPluError => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(CheckPluError)}");
    public string CheckPluWeightCount => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(CheckPluWeightCount)}");
    public string CheckWeightIsEmpty => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(CheckWeightIsEmpty)}");
    public string CheckWeightIsZero => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(CheckWeightIsZero)}");
    public string ClickOnceIntallDirectory => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(ClickOnceIntallDirectory)}");
    public string CommunicateWithAdmin => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(CommunicateWithAdmin)}");
    public string ComPort => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(ComPort)}");
    public string ComPortState => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(ComPort)}");
    public string ContactWithAdmin => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(ContactWithAdmin)}");
    public string Count => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(Count)}");
    public string Crc => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(Crc)}");
    public string Default => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(Default)}");
    public string DeviceControlIsPreview => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(DeviceControlIsPreview)}");
    public string DeviceRegistration => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(DeviceRegistration)}");
    public string Error => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(Error)}");
    public string Exception => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(Exception)}");
    public string ExceptionSqlDb => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(Exception)}");
    public string FieldCurrentTime => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(FieldCurrentTime)}");
    public string FieldDate => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(FieldDate)}");
    public string FieldGln => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(FieldGln)}");
    public string FieldIsIncrementCounter => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(FieldIsIncrementCounter)}");
    public string FieldIsIncrementCounterEnable => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(FieldIsIncrementCounterEnable)}");
    public string FieldKneading => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(FieldKneading)}");
    public string FieldLabelCounter => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(FieldLabelCounter)}");
    public string FieldPalletSize => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(FieldPalletSize)}");
    public string FieldPrintCounter => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(FieldPrintCounter)}");
    public string FieldProductDate => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(FieldProductDate)}");
    public string FieldSscc => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(FieldSscc)}");
    public string FieldSsccControlNumber => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(FieldSsccControlNumber)}");
    public string FieldSynonym => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(FieldSynonym)}");
    public string FieldThresholdLower => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(FieldThresholdLower)}");
    public string FieldThresholdNominal => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(FieldThresholdNominal)}");
    public string FieldThresholds => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(FieldThresholds)}");
    public string FieldThresholdUpper => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(FieldThresholdUpper)}");
    public string FieldTime => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(FieldTime)}");
    public string FieldUnitId => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(FieldUnitId)}");
    public string FieldUnitType => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(FieldUnitType)}");
    public string FieldWeightNetto => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(FieldWeightNetto)}");
    public string FieldWeightTare => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(FieldWeightTare)}");
    public string Host => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(Host)}");
    public string HostUidNotFound => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(HostUidNotFound)}");
    public string HostUidQuestionWriteToDb => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(HostUidNotFound)}");
    public string HostUidQuestionWriteToFile => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(HostUidQuestionWriteToFile)}");
    public string IsConnectWithMassa => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(IsConnectWithMassa)}");
    public string IsDataNotExists => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(IsDataNotExists)}");
    public string IsNotConnectWithMassa => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(IsNotConnectWithMassa)}");
    public string IsShowMaximizeButton => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(IsShowMaximizeButton)}");
    public string IsShowMinimizeButton => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(IsShowMinimizeButton)}");
    public string IsShowPrintButton => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(IsShowPrintButton)}");
    public string LabelContextExpirationDt => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(LabelContextExpirationDt)}");
    public string LabelContextKneading => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(LabelContextKneading)}");
    public string LabelContextNesting => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(LabelContextNesting)}");
    public string LabelContextPlu => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(LabelContextPlu)}");
    public string LabelContextProductDt => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(LabelContextProductDt)}");
    public string LabelContextWeight => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(LabelContextWeight)}");
    public string LabelContextWorkShop => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(LabelContextWorkShop)}");
    public string LabelPrint => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(LabelPrint)}");
    public string Labels => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(Labels)}");
    public string Line => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(Line)}");
    public string LineForHost => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(LineForHost)}");
    public string LowerValue => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(UpperValue)}");
    public string MassaExchange => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(MassaExchange)}");
    public string MassaIsNotCalc => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(MassaIsNotCalc)}");
    public string MassaIsNotFound => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(MassaIsNotFound)}");
    public string MassaIsNotQuering => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(MassaIsNotQuering)}");
    public string MassaIsNotRespond => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(MassaIsNotRespond)}");
    public string MassaK => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(MassaK)}");
    public string MassaKShort => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(MassaKShort)}");
    public string MassaManager => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(MassaManager)}");
    public string MassaWaitStable => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(MassaWaitStable)}");
    public string Memory => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(Memory)}");
    public string MemoryAll => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(MemoryAll)}");
    public string MemoryBusy => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(MemoryBusy)}");
    public string MemoryFree => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(MemoryFree)}");
    public string MemoryPhysical => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(MemoryPhysical)}");
    public string MemoryVirtual => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(MemoryVirtual)}");
    public string Message => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(Message)}");
    public string Method => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(Method)}");
    public string NettoWeight => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(NettoWeight)}");
    public string NewGenderMan => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(NewGenderMan)}");
    public string NewGenderWoman => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(NewGenderWoman)}");
    public string NominalValue => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(NominalValue)}");
    public string NotFoundGenderMan => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(NotFoundGenderMan)}");
    public string NotFoundGenderWoman => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(NotFoundGenderWoman)}");
    public string NotFoundNeutral => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(NotFoundNeutral)}");
    public string OperationControl => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(OperationControl)}");
    public string PackagedInModifiedAtmosphere => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(PackagedInModifiedAtmosphere)}");
    public string Pallet => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(Pallet)}");
    public string Platform => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(Platform)}");
    public string Plu => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(Plu)}");
    public string PluCount => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(PluCount)}");
    public string PluDescriptionNotSet => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(PluDescriptionNotSet)}");
    public string PluDescriptionSet => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(PluDescriptionSet)}");
    public string PluEan13IsNotSet => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(PluEan13IsNotSet)}");
    public string PluginDefault => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(PluginDefault)}");
    public string PluginLabel => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(PluginLabel)}");
    public string PluginMassa => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(PluginMassa)}");
    public string PluginMemory => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(PluginMemory)}");
    public string PluginPrint => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(PluginPrint)}");
    public string PluginPrintTsc => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(PluginPrintTsc)}");
    public string PluginPrintZebra => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(PluginPrintZebra)}");
    public string PluGtin => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(PluGtin)}");
    public string PluGtinIsNotSet => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(PluGtinIsNotSet)}");
    public string PluIsPiece => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(PluIsPiece)}");
    public string PluIsWeight => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(PluIsWeight)}");
    public string PluItf14IsNotSet => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(PluItf14IsNotSet)}");
    public string PluNotSelect => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(PluNotSelect)}");
    public string PluNotSelectWeight => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(PluNotSelectWeight)}");
    public string PluPackageNotSelect => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(PluPackageNotSelect)}");
    public string PluPage => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(PluPage)}");
    public string PluTemplateIsNotSet => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(PluTemplateIsNotSet)}");
    public string PluTemplateIsSet => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(PluTemplateIsSet)}");
    public string PluWeight => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(PluWeight)}");
    public string ProductWeight => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(ProductWeight)}");
    public string ProgramExit => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(ProgramExit)}");
    public string ProgramIsNotFound => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(ProgramIsNotFound)}");
    public string ProgramLoad => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(ProgramLoad)}");
    public string QuestionCloseApp => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(QuestionCloseApp)}");
    public string QuestionPerformOperation => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(QuestionPerformOperation)}");
    public string QuestionRunApp => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(QuestionRunApp)}");
    public string QuestionWriteToDb => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(QuestionWriteToDb)}");
    public string RegistrationIsComplete => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(RegistrationIsComplete)}");
    public string RequestParameters => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(RequestParameters)}");
    public string Restore => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(Restore)}");
    public string RestoreDevice => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(RestoreDevice)}");
    public string ScaleQueue => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(ScaleQueue)}");
    public string ScheduleForNextDay => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(ScaleQueue)}");
    public string ScheduleForNextHour => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(ScheduleForNextHour)}");
    public string ScreenResolution => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(ScreenResolution)}");
    public string SetArea => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(SetArea)}");
    public string ShippingLabels => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(ShippingLabels)}");
    public string StateCorrect => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(StateCorrect)}");
    public string StateDisable => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(StateDisable)}");
    public string StateError => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(StateError)}");
    public string StateIsNotResponsed => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(StateIsNotResponsed)}");
    public string StateIsResponsed => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(StateIsResponsed)}");
    public string SwitchDeviceSettings => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(SwitchDeviceSettings)}");
    public string SwitchKneading => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(SwitchKneading)}");
    public string SwitchLine => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(SwitchLine)}");
    public string SwitchPlu => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(SwitchPlu)}");
    public string SwitchPluLine => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(SwitchPluLine)}");
    public string SwitchPluNesting => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(SwitchPluNesting)}");
    public string Terminal => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(Terminal)}");
    public string TranslationError => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(TranslationError)}");
    public string UpperValue => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(UpperValue)}");
    public string WeightGenderMan => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(WeightGenderMan)}");
    public string WeightGenderWoman => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(WeightGenderWoman)}");
    public string WeightingControl => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(WeightingControl)}");
    public string WeightingIsCalc => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(WeightingIsCalc)}");
    public string WeightingIsStableDescription => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(WeightingIsStableDescription)}");
    public string WeightUnitGr => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(WeightUnitGr)}");
    public string WeightUnitKg => Locale.Translate($"{WsLocalizationUtils.AppLabelPrint}.{nameof(WeightUnitKg)}");

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
    public string RegistrationIsCompleteDot() => $"{RegistrationIsComplete}.";
    public string RegistrationSuccess(string deviceName, string scaleName) => $"{RegistrationIsComplete}.{Environment.NewLine}{Host} '{deviceName}' {AndLine} '{scaleName}' {AreFound}.";
    public string RegistrationWarningLineNotFound(string deviceName) => $"{LineForHost} '{deviceName}' {NotFoundGenderWoman}!";
    public string SetAreaWithParam(long id, string name) => $"{SetArea}: {id} | {name}";
    public string SetLine(long id, string name) => $"{SwitchLine}: {id} | {name}";
    public string SetPlu(int number, string name) => $"{SwitchPlu}: {number} | {name}";
    public string SetPluNesting(int number, string name, short bundleCount) => $"{SwitchPluNesting}: {number} | {name} | {bundleCount}";

    #endregion
}