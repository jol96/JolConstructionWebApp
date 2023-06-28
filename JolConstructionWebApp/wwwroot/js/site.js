$(document).ready(function () {
    btnClick();
});

function btnClick() {
    var currentUrl = window.location.pathname;
    var currentHref = window.location.href.replace(window.location.origin, '');

    $('.btn').each(function () {
        var linkUrl = $(this).attr('href');
        if (linkUrl === currentUrl || linkUrl === currentHref) {
            $(this).addClass('active');
        }
    });
}

function copyToClipboard(elementId) {
    const element = document.getElementById(elementId);
    const text = element.textContent || element.innerText;
    const input = document.createElement('input');
    input.value = text;
    document.body.appendChild(input);
    input.select();
    document.execCommand('copy');
    document.body.removeChild(input);
    showToast('Copied to clipboard!');
}

function showToast(message) {
    const toastContainer = document.getElementById('toastContainer');
    const toast = document.createElement('div');
    toast.classList.add('toast', 'position-fixed', 'bottom-0', 'end-0', 'me-3', 'mb-3');
    toast.setAttribute('role', 'alert');
    toast.setAttribute('aria-live', 'assertive');
    toast.setAttribute('aria-atomic', 'true');
    toast.innerHTML = `
        <div class="toast-body">
          ${message}
        </div>
      `;
    toastContainer.appendChild(toast);
    const bsToast = new bootstrap.Toast(toast);
    bsToast.show();
}

