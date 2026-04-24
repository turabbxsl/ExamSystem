
(function () {
    const STORAGE_KEY = 'sidebar_collapsed';

    if (localStorage.getItem(STORAGE_KEY) === '1') {
        document.body.classList.add('sidebar-collapsed');
    }

    document.addEventListener('DOMContentLoaded', function () {

        const btn = document.getElementById('sidebarToggle');
        if (btn) {
            btn.addEventListener('click', function () {
                const collapsed = document.body.classList.toggle('sidebar-collapsed');
                localStorage.setItem(STORAGE_KEY, collapsed ? '1' : '0');
            });
        }

        const mobileBtn = document.getElementById('mobileSidebarToggle');
        if (mobileBtn) {
            mobileBtn.addEventListener('click', function () {
                document.body.classList.toggle('mobile-sidebar-open');
            });
        }

        document.addEventListener('click', function (e) {
            const sidebar = document.getElementById('sidebar');
            if (
                document.body.classList.contains('mobile-sidebar-open') &&
                sidebar &&
                !sidebar.contains(e.target) &&
                e.target !== mobileBtn
            ) {
                document.body.classList.remove('mobile-sidebar-open');
            }
        });

        const dateEl = document.getElementById('topbarDate');
        if (dateEl) {
            const now = new Date();
            dateEl.textContent = now.toLocaleDateString('az-AZ', {
                weekday: 'long', year: 'numeric', month: 'long', day: 'numeric'
            });
        }
    });
})();

const loader = {
    el: null,
    _get() {
        if (!this.el) this.el = document.getElementById('loaderOverlay');
        return this.el;
    },
    fadeIn(delay = 0) {
        setTimeout(() => this._get()?.classList.add('active'), delay);
    },
    fadeOut() {
        this._get()?.classList.remove('active');
    }
};

axios.interceptors.request.use(config => { loader.fadeIn(); return config; });
axios.interceptors.response.use(
    res  => { loader.fadeOut(); return res; },
    err  => { loader.fadeOut(); return Promise.reject(err); }
);

(function () {
    const container = document.createElement('div');
    container.id = 'toastContainer';
    Object.assign(container.style, {
        position:  'fixed',
        bottom:    '20px',
        right:     '20px',
        zIndex:    '9998',
        display:   'flex',
        flexDirection: 'column',
        gap:       '8px',
        maxWidth:  '340px'
    });
    document.body.appendChild(container);

    const icons = {
        success: 'fa-circle-check text-success',
        error:   'fa-circle-xmark text-danger',
        warning: 'fa-triangle-exclamation text-warning',
        info:    'fa-circle-info text-info'
    };

    window.notify = function (type = 'info', message = '') {
        const icon = icons[type] || icons.info;

        const el = document.createElement('div');
        el.className = 'toast align-items-center border-0 shadow show';
        el.setAttribute('role', 'alert');
        el.innerHTML = `
            <div class="d-flex">
                <div class="toast-body d-flex align-items-center gap-2">
                    <i class="fa-solid ${icon} fa-fw"></i>
                    <span>${message}</span>
                </div>
                <button type="button" class="btn-close me-2 m-auto" data-bs-dismiss="toast"></button>
            </div>`;

        container.appendChild(el);

        const t = new bootstrap.Toast(el, { delay: 4000 });
        t.show();

        el.addEventListener('hidden.bs.toast', () => el.remove());
    };
})();