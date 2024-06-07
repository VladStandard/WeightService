import { DotNetObjectType } from './types/dotnet-object-type.ts'

let middleMouseEventHandler: ((event: MouseEvent) => Promise<void>) | null = null

/**
 * Handles the middle mouse click event by invoking a specified function on a DotNetObjectType.
 *
 * @param {MouseEvent} event - The middle mouse click event.
 * @param {DotNetObjectType} dotNetObjReference - The reference to the DotNetObjectType.
 * @param {string} functionName - The name of the function to be invoked.
 * @return {Promise<void>} A Promise that resolves when the function is invoked.
 */
const handleMiddleMouseEvent = async (
  event: MouseEvent,
  dotNetObjReference: DotNetObjectType,
  functionName: string
): Promise<void> => {
  if (event.button !== 1) return
  await dotNetObjReference.invokeMethodAsync(functionName)
}

/**
 * Subscribes to the middle mouse click event and assigns a handler function to handle the event.
 *
 * @param {DotNetObjectType} dotNetObjReference - The reference to the DotNetObjectType.
 * @param {string} functionName - The name of the function to be invoked when the middle mouse click event occurs.
 * @return {void} This function does not return a value.
 */
window.subscribeMiddleMouseClickEvent = (dotNetObjReference: DotNetObjectType, functionName: string): void => {
  middleMouseEventHandler = (event: MouseEvent) => handleMiddleMouseEvent(event, dotNetObjReference, functionName)
  document.addEventListener('mousedown', middleMouseEventHandler)
}

/**
 * Unsubscribes from the middle mouse click event and removes the handler function.
 *
 * @return {void} This function does not return a value.
 */
window.unsubscribeMiddleMouseClickEvent = (): void => {
  if (middleMouseEventHandler === null) return
  document.removeEventListener('mousedown', middleMouseEventHandler)
}
