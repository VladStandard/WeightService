window.isElementContainsFocusedItem=e=>!!e&&e.contains(document.activeElement);window.animateDialogOpening=async e=>{const t=document.querySelector(`#${e}`)?.dialog;if(!t)return;t.classList.add("ease-in-out");const n=t.animate([{opacity:"0",transform:"scale(.95)"},{opacity:"1",transform:""}],{duration:150});await n.finished},window.animateDialogClosing=async e=>{const t=document.querySelector(`#${e}`)?.dialog;if(!t)return;t.classList.add("ease-in-out");const n=t.animate([{opacity:"1",transform:""},{opacity:"0",transform:"scale(.8)"}],{duration:150});t.style.opacity="0.0",await n.finished};let e=null,t=null,n="";window.subscribeBarcodeEnterEvent=(o,i)=>{t=t=>(async(t,o,i)=>{if(e&&clearInterval(e),"Enter"===t.key)return n&&await o.invokeMethodAsync(i,n),void(n="");"Shift"!==t.key&&(n+=t.key),e=setInterval((()=>n=""),20)})(t,o,i),document.addEventListener("keypress",t)},window.unsubscribeBarcodeEnterEvent=()=>{null!==t&&document.removeEventListener("keypress",t)};const o=e=>{const t=e.querySelector(".width-sub-element"),n=e.querySelector(".width-ref-element");n&&t&&(n.style.width=`${t.offsetWidth}px`)};window.subscribeElementResize=e=>{if(!e)return;const t=()=>o(e);t(),window.addEventListener("resize",t),e.resizeHandler=t},window.updateElementSize=e=>o(e),window.unsubscribeElementResize=e=>{const t=e.resizeHandler;t&&(window.removeEventListener("resize",t),delete e.resizeHandler)},window.switchTheme=e=>{const t=document.documentElement;let n="dark"===e;"system"===e?(localStorage.removeItem("color-theme"),n=window.matchMedia("(prefers-color-scheme: dark)").matches):localStorage.setItem("color-theme",e),t.classList.toggle("dark",n)},window.initializeTheme=()=>{"dark"===localStorage.getItem("color-theme")||!("color-theme"in localStorage)&&window.matchMedia("(prefers-color-scheme: dark)").matches?document.documentElement.classList.add("dark"):document.documentElement.classList.remove("dark")};let i=null;window.subscribeMiddleMouseClickEvent=(e,t)=>{i=n=>(async(e,t,n)=>{1===e.button&&await t.invokeMethodAsync(n)})(n,e,t),document.addEventListener("mousedown",i)},window.unsubscribeMiddleMouseClickEvent=()=>{null!==i&&document.removeEventListener("mousedown",i)};
