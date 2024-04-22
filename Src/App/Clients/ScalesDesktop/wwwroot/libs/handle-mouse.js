let middleMouseEventHandler = null;

const handleMiddleMouseEvent = (event, dotNetObjRef) => {
  if (event.button !== 1) return
  dotNetObjRef.invokeMethodAsync("HandleMiddleMouseClick")
}

export const initializeMiddleMouseEvent = (dotNetObjRef) => {
  middleMouseEventHandler = event => handleMiddleMouseEvent(event, dotNetObjRef);
  document.addEventListener("mousedown", middleMouseEventHandler )
}


export const removeMiddleMouseEvent = () => {
  if (middleMouseEventHandler === null) return
  document.removeEventListener("mousedown", middleMouseEventHandler)
}
