/*
' Copyright (c) 2010  Christoc.com
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 
*/

using System;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Services.Localization;
using DotNetNuke.Security;


namespace com.christoc.dnn.ForumRss
{

	/// -----------------------------------------------------------------------------
	/// <summary>
	/// The ViewForumRss class displays the content
	/// </summary>
	/// -----------------------------------------------------------------------------
	public partial class View : ForumRssModuleBase, IActionable
	{

	#region Event Handlers

		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}

		private void InitializeComponent()
		{
			Load += Page_Load;
		}


		/// -----------------------------------------------------------------------------
		/// <summary>
		/// Page_Load runs when the control is loaded
		/// </summary>
		/// -----------------------------------------------------------------------------
		private void Page_Load(object sender, EventArgs e)
		{
			try
			{

			}
			catch (Exception exc) //Module failed to load
			{
				Exceptions.ProcessModuleLoadException(this, exc);
			}
		}

	#endregion
		
	#region Optional Interfaces

    public ModuleActionCollection ModuleActions
        {
            get
            {
                var moduleActionCollection = new ModuleActionCollection
                                                 {
                                                     {
                                                         GetNextActionID(),
                                                         Localization.GetString("EditModule", LocalResourceFile),
                                                         "", "", "", EditUrl(), false, SecurityAccessLevel.Edit, true,
                                                         false
                                                         }
                                                 };
                return moduleActionCollection;
            }
        }

	#endregion

	}

}
