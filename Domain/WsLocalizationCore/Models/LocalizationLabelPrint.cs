using WsLocalizationCore.Common;
using WsLocalizationCore.Utils;
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

    private string SwitchMore => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(SwitchMore)}");
    public string AlreadyRunning => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(AlreadyRunning)}");
    public string AndArm => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(AndArm)}");
    public string AndLine => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(AndLine)}");
    public string App => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(App)}");
    public string AppExit => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(AppExit)}");
    public string AppExitDescription => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(AppExitDescription)}");
    public string AppLoad => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(AppLoad)}");
    public string AppLoadDescription => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(AppLoadDescription)}");
    public string AppWait => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(AppWait)}");
    public string AreFound => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(AreFound)}");
    public string AreNotFound => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(AreNotFound)}");
    public string Bundle => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(Bundle)}");
    public string ButtonAddKneading => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(ButtonAddKneading)}");
    public string ButtonPlu => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(ButtonPlu)}");
    public string ButtonScalesInit => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(ButtonScalesInit)}");
    public string ButtonSetKneading => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(ButtonSetKneading)}");
    public string ButtonSettings => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(ButtonSettings)}");
    public string CheckAllPassed => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(CheckAllPassed)}");
    public string CheckPluError => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(CheckPluError)}");
    public string CheckPluWeightCount => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(CheckPluWeightCount)}");
    public string CheckWeightIsEmpty => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(CheckWeightIsEmpty)}");
    public string CheckWeightIsZero => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(CheckWeightIsZero)}");
    public string ClickOnceIntallDirectory => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(ClickOnceIntallDirectory)}");
    public string CommunicateWithAdmin => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(CommunicateWithAdmin)}");
    public string ComPort => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(ComPort)}");
    public string ComPortState => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(ComPort)}");
    public string ContactWithAdmin => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(ContactWithAdmin)}");
    public string Count => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(Count)}");
    public string Crc => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(Crc)}");
    public string Default => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(Default)}");
    public string DeviceControlIsPreview => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(DeviceControlIsPreview)}");
    public string DeviceRegistration => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(DeviceRegistration)}");
    public string Error => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(Error)}");
    public string Exception => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(Exception)}");
    public string ExceptionSqlDb => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(Exception)}");
    public string FieldCurrentTime => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(FieldCurrentTime)}");
    public string FieldDate => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(FieldDate)}");
    public string FieldGln => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(FieldGln)}");
    public string FieldIsIncrementCounter => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(FieldIsIncrementCounter)}");
    public string FieldIsIncrementCounterEnable => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(FieldIsIncrementCounterEnable)}");
    public string FieldKneading => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(FieldKneading)}");
    public string FieldLabelCounter => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(FieldLabelCounter)}");
    public string FieldPalletSize => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(FieldPalletSize)}");
    public string FieldPrintCounter => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(FieldPrintCounter)}");
    public string FieldProductDate => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(FieldProductDate)}");
    public string FieldSscc => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(FieldSscc)}");
    public string FieldSsccControlNumber => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(FieldSsccControlNumber)}");
    public string FieldSynonym => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(FieldSynonym)}");
    public string FieldThresholdLower => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(FieldThresholdLower)}");
    public string FieldThresholdNominal => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(FieldThresholdNominal)}");
    public string FieldThresholds => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(FieldThresholds)}");
    public string FieldThresholdUpper => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(FieldThresholdUpper)}");
    public string FieldTime => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(FieldTime)}");
    public string FieldUnitId => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(FieldUnitId)}");
    public string FieldUnitType => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(FieldUnitType)}");
    public string FieldWeightNetto => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(FieldWeightNetto)}");
    public string FieldWeightTare => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(FieldWeightTare)}");
    public string Host => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(Host)}");
    public string HostUidNotFound => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(HostUidNotFound)}");
    public string HostUidQuestionWriteToDb => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(HostUidNotFound)}");
    public string HostUidQuestionWriteToFile => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(HostUidQuestionWriteToFile)}");
    public string IsConnectWithMassa => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(IsConnectWithMassa)}");
    public string IsDataNotExists => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(IsDataNotExists)}");
    public string IsNotConnectWithMassa => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(IsNotConnectWithMassa)}");
    public string IsShowMaximizeButton => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(IsShowMaximizeButton)}");
    public string IsShowMinimizeButton => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(IsShowMinimizeButton)}");
    public string IsShowPrintButton => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(IsShowPrintButton)}");
    public string LabelContextExpirationDt => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(LabelContextExpirationDt)}");
    public string LabelContextKneading => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(LabelContextKneading)}");
    public string LabelContextNesting => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(LabelContextNesting)}");
    public string LabelContextPlu => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(LabelContextPlu)}");
    public string LabelContextProductDt => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(LabelContextProductDt)}");
    public string LabelContextWeight => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(LabelContextWeight)}");
    public string LabelContextWorkShop => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(LabelContextWorkShop)}");
    public string LabelPrint => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(LabelPrint)}");
    public string Labels => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(Labels)}");
    public string Line => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(Line)}");
    public string LineForHost => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(LineForHost)}");
    public string LowerValue => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(UpperValue)}");
    public string MassaExchange => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(MassaExchange)}");
    public string MassaIsNotCalc => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(MassaIsNotCalc)}");
    public string MassaIsNotFound => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(MassaIsNotFound)}");
    public string MassaIsNotQuering => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(MassaIsNotQuering)}");
    public string MassaIsNotRespond => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(MassaIsNotRespond)}");
    public string MassaK => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(MassaK)}");
    public string MassaKShort => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(MassaKShort)}");
    public string MassaManager => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(MassaManager)}");
    public string MassaWaitStable => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(MassaWaitStable)}");
    public string Memory => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(Memory)}");
    public string MemoryAll => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(MemoryAll)}");
    public string MemoryBusy => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(MemoryBusy)}");
    public string MemoryFree => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(MemoryFree)}");
    public string MemoryPhysical => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(MemoryPhysical)}");
    public string MemoryVirtual => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(MemoryVirtual)}");
    public string Message => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(Message)}");
    public string Method => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(Method)}");
    public string NettoWeight => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(NettoWeight)}");
    public string NewGenderMan => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(NewGenderMan)}");
    public string NewGenderWoman => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(NewGenderWoman)}");
    public string NominalValue => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(NominalValue)}");
    public string NotFoundGenderMan => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(NotFoundGenderMan)}");
    public string NotFoundGenderWoman => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(NotFoundGenderWoman)}");
    public string NotFoundNeutral => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(NotFoundNeutral)}");
    public string OperationControl => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(OperationControl)}");
    public string PackagedInModifiedAtmosphere => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(PackagedInModifiedAtmosphere)}");
    public string Pallet => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(Pallet)}");
    public string Platform => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(Platform)}");
    public string Plu => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(Plu)}");
    public string PluCount => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(PluCount)}");
    public string PluDescriptionNotSet => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(PluDescriptionNotSet)}");
    public string PluDescriptionSet => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(PluDescriptionSet)}");
    public string PluEan13IsNotSet => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(PluEan13IsNotSet)}");
    public string PluginDefault => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(PluginDefault)}");
    public string PluginLabel => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(PluginLabel)}");
    public string PluginMassa => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(PluginMassa)}");
    public string PluginMemory => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(PluginMemory)}");
    public string PluginPrint => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(PluginPrint)}");
    public string PluginPrintTsc => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(PluginPrintTsc)}");
    public string PluginPrintZebra => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(PluginPrintZebra)}");
    public string PluGtin => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(PluGtin)}");
    public string PluGtinIsNotSet => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(PluGtinIsNotSet)}");
    public string PluIsPiece => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(PluIsPiece)}");
    public string PluIsWeight => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(PluIsWeight)}");
    public string PluItf14IsNotSet => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(PluItf14IsNotSet)}");
    public string PluNotSelect => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(PluNotSelect)}");
    public string PluNotSelectWeight => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(PluNotSelectWeight)}");
    public string PluPackageNotSelect => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(PluPackageNotSelect)}");
    public string PluPage => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(PluPage)}");
    public string PluTemplateIsNotSet => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(PluTemplateIsNotSet)}");
    public string PluTemplateIsSet => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(PluTemplateIsSet)}");
    public string PluWeight => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(PluWeight)}");
    public string ProductWeight => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(ProductWeight)}");
    public string ProgramExit => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(ProgramExit)}");
    public string ProgramIsNotFound => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(ProgramIsNotFound)}");
    public string ProgramLoad => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(ProgramLoad)}");
    public string QuestionCloseApp => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(QuestionCloseApp)}");
    public string QuestionPerformOperation => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(QuestionPerformOperation)}");
    public string QuestionRunApp => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(QuestionRunApp)}");
    public string QuestionWriteToDb => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(QuestionWriteToDb)}");
    public string RegistrationIsComplete => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(RegistrationIsComplete)}");
    public string RequestParameters => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(RequestParameters)}");
    public string Restore => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(Restore)}");
    public string RestoreDevice => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(RestoreDevice)}");
    public string ScaleQueue => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(ScaleQueue)}");
    public string ScheduleForNextDay => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(ScaleQueue)}");
    public string ScheduleForNextHour => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(ScheduleForNextHour)}");
    public string ScreenResolution => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(ScreenResolution)}");
    public string SetArea => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(SetArea)}");
    public string ShippingLabels => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(ShippingLabels)}");
    public string StateCorrect => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(StateCorrect)}");
    public string StateDisable => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(StateDisable)}");
    public string StateError => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(StateError)}");
    public string StateIsNotResponsed => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(StateIsNotResponsed)}");
    public string StateIsResponsed => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(StateIsResponsed)}");
    public string SwitchDeviceSettings => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(SwitchDeviceSettings)}");
    public string SwitchKneading => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(SwitchKneading)}");
    public string SwitchLine => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(SwitchLine)}");
    public string SwitchPlu => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(SwitchPlu)}");
    public string SwitchPluLine => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(SwitchPluLine)}");
    public string SwitchPluNesting => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(SwitchPluNesting)}");
    public string Terminal => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(Terminal)}");
    public string TranslationError => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(TranslationError)}");
    public string UpperValue => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(UpperValue)}");
    public string WeightGenderMan => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(WeightGenderMan)}");
    public string WeightingControl => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(WeightingControl)}");
    public string WeightingIsCalc => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(WeightingIsCalc)}");
    public string WeightingIsStableDescription => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(WeightingIsStableDescription)}");
    public string WeightUnitGr => Locale.Translate($"{LocalizationUtils.AppLabelPrint}.{nameof(WeightUnitGr)}");
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
    public string RegistrationIsCompleteDot() => $"{RegistrationIsComplete}.";
    public string RegistrationSuccess(string deviceName, string scaleName) => $"{RegistrationIsComplete}.{Environment.NewLine}{Host} '{deviceName}' {AndLine} '{scaleName}' {AreFound}.";
    public string RegistrationWarningLineNotFound(string deviceName) => $"{LineForHost} '{deviceName}' {NotFoundGenderWoman}!";
    public string SetAreaWithParam(long id, string name) => $"{SetArea}: {id} | {name}";
    public string SetLine(long id, string name) => $"{SwitchLine}: {id} | {name}";
    public string SetPlu(int number, string name) => $"{SwitchPlu}: {number} | {name}";
    public string SetPluNesting(int number, string name, short bundleCount) => $"{SwitchPluNesting}: {number} | {name} | {bundleCount}";

    #endregion
}