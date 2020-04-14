self.addEventListener('message', function (e) {

    //we get an error when we put var before __BRYTHON__ below
    __BRYTHON__ = { isa_web_worker: true }

    //importScripts('brython.js'/*tpa=https://brython.info/src/web_workers/brython.js*/)
    importScripts('../brython_builtins.js'/*tpa=https://brython.info/src/brython_builtins.js*/, '../version_info.js'/*tpa=https://brython.info/src/version_info.js*/,
        '../py2js.js'/*tpa=https://brython.info/src/py2js.js*/,
        '../py_object.js'/*tpa=https://brython.info/src/py_object.js*/, '../py_type.js'/*tpa=https://brython.info/src/py_type.js*/, '../py_utils.js'/*tpa=https://brython.info/src/py_utils.js*/,
        '../py_builtin_functions.js'/*tpa=https://brython.info/src/py_builtin_functions.js*/, '../py_set.js'/*tpa=https://brython.info/src/py_set.js*/,
        '../js_objects.js'/*tpa=https://brython.info/src/js_objects.js*/,
        '../py_import.js'/*tpa=https://brython.info/src/py_import.js*/, '../py_int.js'/*tpa=https://brython.info/src/py_int.js*/, '../py_float.js'/*tpa=https://brython.info/src/py_float.js*/,
        '../py_complex.js'/*tpa=https://brython.info/src/py_complex.js*/,
        '../py_dict.js'/*tpa=https://brython.info/src/py_dict.js*/, '../py_list.js'/*tpa=https://brython.info/src/py_list.js*/, '../py_string.js'/*tpa=https://brython.info/src/py_string.js*/);

    __BRYTHON__.$py_src = {}

    // Mapping between a module name and its path (url)
    __BRYTHON__.$py_module_path = {}

    // path_hook used in py_import.js
    __BRYTHON__.path_hooks = []

    // Maps a module name to matching module object
    // A module can be the body of a script, or the body of a block inside a
    // script, such as in exec() or in a comprehension
    __BRYTHON__.modules = {}

    // Maps the name of imported modules to the module object
    __BRYTHON__.imported = {}

    // Options passed to brython(), with default values
    __BRYTHON__.$options = {}

    // Used to compute the hash value of some objects (see
    // py_builtin_functions.js)
    __BRYTHON__.$py_next_hash = -Math.pow(2, 53)

    __BRYTHON__.$options = {}
    __BRYTHON__.debug = __BRYTHON__.$options.debug = 0

    // Stacks for exceptions and function calls, used for exception handling
    __BRYTHON__.call_stack = []

    // Maps a Python block (module, function, class) to a Javascript object
    // mapping the names defined in this block to their value
    __BRYTHON__.vars = {}

    // capture all standard output
    var output = []
    __BRYTHON__.stdout = {
        write: function (data) {
            output.push(data)
        }
    }

    // insert already defined builtins
    for (var $py_builtin in __BRYTHON__.builtins) {
        eval("var " + $py_builtin + "=__BRYTHON__.builtins[$py_builtin]")
    }

    var $defaults = {}
    eval('var _result=(' + e.data.target + ')(' + e.data.args + ')')


    if (e.data.pos !== undefined) {
        // allows parent to know where this individual result belongs in the
        // result list
        self.postMessage({
            stdout: output.join(''),
            result: _result,
            pos: e.data.pos
        })
    } else {
        self.postMessage({
            stdout: output.join(''),
            result: _result.toString()
        })
    }

}, false);
