﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AndromedaStudio.Data.Controls
{
    /// <summary>
    /// EN: Represents a control that displays SVG icons.
    /// RU: Представляет элемент управления, отображающий SVG иконки.
    /// </summary>
    public partial class Icon : UserControl
    {
        public Icon()
        {
            InitializeComponent();
        }

        #region Properties

        /// <summary>
        /// EN: Sets the geometry value.
        /// RU: Задает значение геометрии.
        /// </summary>
        public Geometry Data
        {
            set => Container.Data = value;
        }

        /// <summary>
        /// EN: Sets the System.Windows.Media.Brush
        /// RU: Задает System.Windows.Media.Brush
        /// </summary>
        public Brush Fill
        {
            set => Container.Fill = value;
        }

        /// <summary>
        /// EN: Gets or sets the value of the external content field.
        /// RU: Получает или задает значение внешнего поля содержимого.
        /// </summary>
        public Thickness IconMargin
        {
            get => Container.Margin;
            set => Container.Margin = value;
        }

        #endregion
    }
}