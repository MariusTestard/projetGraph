using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System.Xml.Linq;
using Windows.ApplicationModel.Contacts;
using System.Collections.ObjectModel;


// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ProjetFinal
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AfficherClients : Page
    {
        public AfficherClients()
        {
            this.InitializeComponent();
            lvListeClients.ItemsSource = SingletonClient.getInstance().getListeClients();
        }

        // Whenever text changes in any of the filtering text boxes, the following function is called:
        private void OnFilterChanged(object sender, TextChangedEventArgs args)
        {
            // This is a Linq query that selects only items that return True after being passed through
            // the Filter function, and adds all of those selected items to filtered.
            var filtered = SingletonClient.getInstance().getListeClients().Where(client => Filter(client));
            Remove_NonMatching(filtered);
            AddBack_Contacts(filtered);
        }

        private bool Filter(Client client)
        {
            return client.Nom.Contains(FilterByName.Text, StringComparison.InvariantCultureIgnoreCase);
        }

        /* These functions go through the current list being displayed (contactsFiltered), and remove
        any items not in the filtered collection (any items that don't belong), or add back any items
        from the original allContacts list that are now supposed to be displayed (i.e. when backspace is hit). */
        private void Remove_NonMatching(IEnumerable<Client> filteredData)
        {
            for (int i = SingletonClient.getInstance().getListeClients().Count - 1; i >= 0; i--)
            {
                var item = SingletonClient.getInstance().getListeClients()[i];
                // If contact is not in the filtered argument list, remove it from the ListView's source.
                if (!filteredData.Contains(item))
                {
                    SingletonClient.getInstance().getListeClients().Remove(item);
                }
            }
        }

        private void AddBack_Contacts(IEnumerable<Client> filteredData)
        {
            foreach (var item in filteredData)
            {
                // If item in filtered list is not currently in ListView's source collection, add it back in
                if (!SingletonClient.getInstance().getListeClients().Contains(item))
                {
                    SingletonClient.getInstance().getListeClients().Add(item);
                }
            }
        }
    }
}
