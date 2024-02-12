export const initializeResizeSelect = (element) => {
  if (!element) return
  const resizeHandler = () => resizeSelect(element);
  resizeHandler()
  window.addEventListener('resize', resizeHandler)
  element.__resizeHandler = resizeHandler
}

export const removeResizeEvent = (element) => {
  const resizeHandler = element.__resizeHandler;
  if (resizeHandler) {
    window.removeEventListener('resize', resizeHandler);
    delete element.__resizeHandler;
  }
};

const resizeSelect = (element) => {
  const button = element.querySelector('.select-button')
  const dropdown = element.querySelector('.select-dropdown')
  if (dropdown && button) dropdown.style.width = `${button.offsetWidth}px`
}
