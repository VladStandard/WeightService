const TIMEOUT_INTERVAL = 50

let timeoutId
let inputEventHandler = null
let barcode = ''


const handleBarcodeEnter = (event, dotNetObjRef) => {
  if (timeoutId) clearInterval(timeoutId)

  if (event.key === 'Enter') {
    if (barcode)
      dotNetObjRef.invokeMethodAsync("HandleInputEvent", barcode)
    barcode = ''
    return
  }

  if (event.key !== 'Shift')
    barcode += event.key

  timeoutId = setInterval(() => barcode = '', 20)
}

export const initializeBarcodeEnterEvent = (dotNetObjRef) => {
  inputEventHandler = event => handleBarcodeEnter(event, dotNetObjRef)
  document.addEventListener("keypress", inputEventHandler)
}


export const removeBarcodeEnterEvent = () => {
  if (inputEventHandler === null) return
  document.removeEventListener("keypress", inputEventHandler)
}

