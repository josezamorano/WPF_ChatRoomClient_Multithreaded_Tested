   M S B A M L   `   `   `  ����L  HPresentationLayer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null[2clr-namespace:PresentationLayer.CustomUserControls$PresentationLayer.CustomUserControls  5      ;    5PresentationLayer.CustomUserControls.AllItemsListView   5      W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Bcuc2clr-namespace:PresentationLayer.CustomUserControls     5      -   5        $!��AllItemsListViewUserControl��5      .��5
      �� -   5          ��Name$  BorderAllItemsListView��5
      
���  ��$��0q�6?   ._�5   
   � -   5         �Name$ GridAllItemsListView��5      .+�5      �� -   5         ��Name$ ListViewAllItems��5      
������     panelBackgroundBrushGrey_III#��[  5      ��5      ��  �� Path$ ListViewItemsSource� �� RelativeSource ��  �� AncestorType;   5PresentationLayer.CustomUserControls.AllItemsListView"   �� SelectedItem 5      �� $ ListViewSelectedItem� �� *FindAncestor+"   �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�	 � 	Resources	 5      
 �� 
TargetType �� Property �� Value �� Triggers P� Property P� Value   panelBackgroundBrushGrey_XVI��%@  '��       '�� �     7  �� 5      "
 ��6   .�5      ��  �@��CornerRadius$ 5��69   �� 5       � ��
 �����69   �� 5       � ��$ 5q�6<   5      �� 5      "
 ��6    5      P� 5       �@��
IsSelected$
 True��6<   . �5   "   ��  � ��8   6?   5      5      5       ��5"      �� 5#      "
 ��6   .�5$      ��  � ��$ Stretch=�6G    5%      P� 5&       �@��
IsSelected$
 True��6<   . �5'   "   ��  � ��# [ 6?   5(      5,      ��5.      �� 5/      .R�50      ��    panelBackgroundBrushGrey_VII#��[ 6!   $��3q�51   !    �� CornerRadius$ 5��52   !   ._�55      � $ GridListViewItems��6#   $��Stretch=�56   #   #��[ 57   #    � ColumnDefinitions 59   "   �� 5:   &   $��2* �67   �� 5;   &   $��8* �67    � RowDefinitions 5=   "   �� 5>   &   $��* �64   �� 5?   &   $��* �64   �� 5@   &   $��* �64   .+�5C   "   ��  ��Name$ TextBlockChatRoomName��6,   $��0��5D   ,   $��0��69   $��
ChatRoom: ��5E   ,     standardTextBlock#��[ 5F   ,   $��Right=�5G   ,   $��Center=�5H   ,   $��0,0,2,0q�5I   ,   
���   �5J   ,   �� 5L   "   $ TextBoxChatRoomName��6,   $��0��5M   ,   $��1��69   #��[ 5O   ,   $
��left=�5P   ,   $��Center=�5Q   ,   $��5,0,0,0q�5R   ,   
���   ���5N   ,   �� *ChatRoomName+$ TwoWay=�$ PropertyChanged=�5S   ,   �� 5U   "   $ TextBlockChatRoomID��6,   $��1��5V   ,   $��0��69   $��ChatRoom ID:��5W   ,   #��[ 5X   ,   $��Right=�5Y   ,   $��Center=�5Z   ,   $��0,0,5,0q�5[   ,   
���   �5\   ,   �� 5^   "   $ TextBoxChatRoomID��6,   $��1��5_   ,   $��1��69   #��[ 5a   ,   $
��left=�5b   ,   $��Center=�5c   ,   $��5,0,0,0q�5d   ,   
���   ���5`   ,   �� *
ChatRoomId+$ TwoWay=�$ PropertyChanged=�5e   ,   �� 5g   "   $ TextBlockTotalGuests��6,   $��2��5h   ,   $��0��69   $��Active Users:��5i   ,   #��[ 5j   ,   $��Right=�5k   ,   $��Center=�5l   ,   $��0,0,5,0q�5m   ,   
���   �5n   ,   �� 5p   "   $ TextBoxTotalGuests��6,   $��2��5q   ,   $��1��69   #��[ 5s   ,   $
��left=�5t   ,   $��Center=�5u   ,   $��5,0,0,0q�5v   ,   
���   ���5r   ,   �� *AllActiveUsersInChatRoomCount+$ TwoWay=�$ PropertyChanged=�5w   ,   5y      5{      5|      5      5�      5�      5�      