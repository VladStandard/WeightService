import { DotNetObjectType } from './types/dotnet-object-type.ts'

const TIMEOUT_INTERVAL = 20

let timeoutId: ReturnType<typeof setInterval> | null = null
let inputEventHandler: ((event: KeyboardEvent) => void) | null = null
let barcode: string = ''

/**
 * Handles the event when a barcode is entered.
 *
 * @param {KeyboardEvent} event - The keyboard event triggered by the barcode input.
 * @param {DotNetObjectType} dotNetObjReference - The reference to the DotNetObjectType.
 * @param {string} functionName - The name of the function to be invoked.
 * @return {Promise<void>} A promise that resolves when the barcode is handled.
 */
const handleBarcodeEnter = async (
  event: KeyboardEvent,
  dotNetObjReference: DotNetObjectType,
  functionName: string
): Promise<void> => {
  if (timeoutId) clearInterval(timeoutId)

  if (event.key === 'Enter') {
    if (barcode) await dotNetObjReference.invokeMethodAsync(functionName, barcode)
    barcode = ''
    return
  }

  if (event.key !== 'Shift') barcode += event.key

  timeoutId = setInterval(() => (barcode = ''), TIMEOUT_INTERVAL)
}

/**
 * Subscribes to the barcode enter event.
 *
 * @param {DotNetObjectType} dotNetObjReference - The reference to the DotNetObjectType.
 * @param {string} functionName - The name of the function to be invoked.
 * @return {void} This function does not return anything.
 */
window.subscribeBarcodeEnterEvent = (dotNetObjReference: DotNetObjectType, functionName: string): void => {
  inputEventHandler = (event) => handleBarcodeEnter(event, dotNetObjReference, functionName)
  document.addEventListener('keypress', inputEventHandler)
}

/**
 * Unsubscribes from the barcode enter event.
 *
 * @return {void} This function does not return anything.
 */
window.unsubscribeBarcodeEnterEvent = (): void => {
  if (inputEventHandler === null) return
  document.removeEventListener('keypress', inputEventHandler)
}
