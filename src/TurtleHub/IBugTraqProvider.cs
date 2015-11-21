namespace Interop.BugTraqProvider
{
    using System;
    using System.Runtime.InteropServices;

    //
    // Definition adapted from:
    // http://tortoisesvn.tigris.org/svn/tortoisesvn/trunk/contrib/issue-tracker-plugins/Interop.BugTraqProvider/IBugTraqProvider.cs
    //
    // Original IDL definition can be found at:
    // http://tortoisesvn.tigris.org/svn/tortoisesvn/trunk/contrib/issue-tracker-plugins/inc/IBugTraqProvider.idl
    //
    // See also:
    // http://tortoisesvn.net/docs/release/TortoiseSVN_en/tsvn-ibugtraqprovider.html#tsvn-ibugtraqprovider-1
    //

    /// <remarks>
    /// While the rest of TortoiseSVN is licensed under the GPL,
    /// this portion is public domain.
    /// </remarks>

    [ ComImport ]
    [ InterfaceType(ComInterfaceType.InterfaceIsIUnknown) ]
    [ Guid("298B927C-7220-423C-B7B4-6E241F00CD93") ]
    internal interface IBugTraqProvider
    {
        /// <summary>
        /// The ValidateParameters method is called from the settings 
        /// dialog. This allows the provider to check that the parameters 
        /// are OK. The provider can do basic syntax checking, it can check 
        /// that the server is reachable, or it can do nothing.
        /// </summary>
        /// <param name="hParentWnd">
        /// Parent window for any UI that needs to be displayed during validation.</param>
        /// <param name="parameters">
        /// The parameter string that needs to be validated.</param>
        /// <returns>
        /// Is the string valid?</returns>
        /// <remarks>
        /// <para>
        /// A provider might need some parameters (e.g. a web service URL 
        /// or a database connection string). This information is passed as 
        /// a simple string. It's up to the individual provider to parse and
        /// validate it.</para>
        /// <para>
        /// If the provider needs to report a validation error, it should do this itself, using hParentWnd as
        /// the parent of any displayed UI.
        /// </para>
        /// <code>
        /// HRESULT ValidateParameters (
        ///     [in] HWND hParentWnd,               // Parent window for any UI that needs to be displayed during validation.
        ///     [in] BSTR parameters,               // The parameter string that needs to be validated.
        ///     [out, retval] VARIANT_BOOL *valid   // Is the string valid?
        /// );
        /// </code>
        /// </remarks>

        [return: MarshalAs(UnmanagedType.VariantBool)]
        bool ValidateParameters(IntPtr hParentWnd,
            [MarshalAs(UnmanagedType.BStr)] string parameters);

        /// <summary>
        /// In the commit dialog, the provider is accessed from a button. It 
        /// needs to know what text to display (e.g. "Choose Bug" or 
        /// "Select Ticket").
        /// </summary>
        /// <param name="hParentWnd">
        /// Parent window for any (error) UI that needs to be displayed.</param>
        /// <param name="parameters">
        /// The parameter string, just in case you need to talk to your web 
        /// service (e.g.) to find out what the correct text is.</param>
        /// <returns>
        /// Text to display using the current thread locale.</returns>
        /// <remarks>
        /// <code>
        /// HRESULT GetLinkText (
        ///         [in] HWND hParentWnd,           // Parent window for any (error) UI that needs to be displayed.
        ///         [in] BSTR parameters,           // The parameter string, just in case you need to talk to your web
        ///                                         // service (e.g.) to find out what the correct text is.
        ///         [out, retval] BSTR *linkText    // What text do you want to display? Use the current thread locale.
        ///     );
        /// </code>
        /// </remarks>

        [return: MarshalAs(UnmanagedType.BStr)]
        string GetLinkText(IntPtr hParentWnd,
            [MarshalAs(UnmanagedType.BStr)] string parameters);

        /// <summary>
        /// Get the commit message. This would normally involve displaying a 
        /// dialog with a list of the issues assigned to the current user.
        /// </summary>
        /// <param name="hParentWnd">
        /// Parent window for your provider's UI.</param>
        /// <param name="parameters">
        /// Parameters for your provider.</param>
        /// <param name="commonRoot"></param>
        /// <param name="pathList"></param>
        /// <param name="originalMessage">
        /// The text already present in the commit message.
        /// Your provider should include this text in the new message, where 
        /// appropriate.</param>
        /// <returns>
        /// The new text for the commit message. This replaces the original 
        /// message.</returns>
        /// <remarks>
        /// <code>
        /// HRESULT GetCommitMessage (
        ///     [in] HWND hParentWnd,           // Parent window for your provider's UI.
        ///     [in] BSTR parameters,           // Parameters for your provider.
        ///     [in] BSTR commonRoot,
        ///     [in] SAFEARRAY(BSTR) pathList,
        ///     [in] BSTR originalMessage,      // The text already present in the commit message.
        ///                                     // Your provider should include this text in the new message, where appropriate.
        ///     [out, retval] BSTR *newMessage  // The new text for the commit message. This replaces the original message.
        /// );
        /// </code>
        /// </remarks>

        [return: MarshalAs(UnmanagedType.BStr)]
        string GetCommitMessage(IntPtr hParentWnd,
            [MarshalAs(UnmanagedType.BStr)] string parameters,
            [MarshalAs(UnmanagedType.BStr)] string commonRoot,
            [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_BSTR)] string[] pathList,
            [MarshalAs(UnmanagedType.BStr)] string originalMessage);
    }
}
