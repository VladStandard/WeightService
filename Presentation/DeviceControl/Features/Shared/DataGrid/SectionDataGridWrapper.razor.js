let eventHandler

export const addClickOutsideListener = (elementId, dotnetHelper) => {
  eventHandler = (event) => {
    const element = document.getElementById(elementId);
    if (element === null || element.contains(event.target)) return;
    dotnetHelper.invokeMethodAsync('ContextMenuClickedOutsideAction');
  };
  document.addEventListener('click', eventHandler)
}

export const removeClickOutsideListener = () =>
  document.removeEventListener('click', eventHandler)
